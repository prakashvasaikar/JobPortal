app.controller("Career", function (careerFactory) {
    var vm = this;

    vm.objCareerList = [];
    vm.objCareer = {};
    vm.JobName = "";
    var modal;
    vm.isLoading = false;
    vm.loadJobRequirement = function () {
        vm.isLoading = true;
        careerFactory.getJobRequirementList()
            .then(res => {
                vm.objCareerList = res.data;
            })
            .catch(err => console.error("jobRequirement list error", err))
            .finally(() => vm.isLoading = false);
    };

    vm.applyJob = function (id, vacancyType) {
        var user = localStorage.getItem("User");
        var role = localStorage.getItem("Role");
        var userId = localStorage.getItem("UserId");
        vm.JobName = vacancyType;
        if (user === null || user.trim() === "") {
            alert("Please register");
            return false;
        }
        if (role === "Admin") {
            alert("Admin can't apply job");
            return false;
        }
        vm.objCareer = {
            id: 0,
            refId_CompanyRequirement: id,
            refId_UserMaster: userId,
            primarySkill: "",
            status: "Pending",
            experienceYear: 0,
            experienceMonth: 0,
            referBy: "",
        };

        modal.show();
    };

    vm.saveApplyJob = function () {
        vm.isLoading = true;
        const file = vm.objCareer.resume;
        const primarySkill = vm.objCareer.primarySkill;
        const years = vm.objCareer.experienceYear;
        const months = vm.objCareer.experienceMonth;

        if (!primarySkill || primarySkill.trim() === "") {
            alert("Primary Skill is required.");
            return;
        }

        if (primarySkill.length < 3) {
            alert("Primary Skill must be at least 3 characters long.");
            return;
        }

        const skillRegex = /^[A-Za-z\s\.\,]+$/;
        if (!skillRegex.test(primarySkill)) {
            alert("Primary Skill should contain only letters.");
            return;
        }

        if (years == null || months == null) {
            alert("Please enter both years and months of experience.");
            return;
        }
        if (isNaN(years) || isNaN(months)) {
            alert("Experience must be numeric.");
            return;
        }
        if (years < 0 || years > 50) {
            alert("Years of experience must be between 0 and 50.");
            return;
        }
        if (months < 0 || months > 11) {
            alert("Months of experience must be between 0 and 11.");
            return;
        }
        if (years === 0 && months === 0) {
            alert("Experience cannot be 0 years and 0 months.");
            return;
        }

        if (!file) {
            alert("Please upload a PDF file before applying.");
            return;
        }

        const fileExtension = file.name.split('.').pop().toLowerCase();
        if (fileExtension !== 'pdf') {
            alert("Invalid file type. Only PDF files are allowed.");
            return;
        }

        const maxSize = 2 * 1024 * 1024; // 2 MB
        if (file.size > maxSize) {
            alert("File size exceeds 2 MB limit.");
            return;
        }

        careerFactory.saveApplyJob(vm.objCareer)
            .then(res => {
                if (res.data.success) {
                    alert("Apply Successfully !!!");
                    bootstrap.Modal.getInstance(document.getElementById('careerModal')).hide();
                } else if (!res.data.success) {
                    alert(res.data.message);
                }
                else {
                    alert("Something went wrong: " + res.data.message);
                }
            })
            .catch(err => console.error("Internal server error", err))
            .finally(() => vm.isLoading = false);

    };

    setTimeout(() => {
        modal = new bootstrap.Modal(document.getElementById("careerModal"));
    }, 500);
    vm.loadJobRequirement();
});
