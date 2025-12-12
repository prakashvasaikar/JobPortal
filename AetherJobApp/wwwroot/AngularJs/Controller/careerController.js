app.controller("Career", function (careerFactory) {
    var vm = this;

    vm.objCareerList = [];
    vm.objCareer = {};
    vm.objVacancyLists = [];
    vm.JobName = "";
    vm.isLoading = false;

    var modal;

    // ------------------------------------------
    // INITIALIZE (called only from ng-init)
    // ------------------------------------------
    vm.init = function () {
        vm.objCareer = {
            refId_VacancyMaster: ""
        };

        // Load Vacancy List (Dropdown)
        careerFactory.getActiveVacancyList()
            .then(res => {
                debugger
                vm.objVacancyLists = res.data;
               
                setTimeout(() => {
                    $('select').niceSelect('destroy');
                    $('select').niceSelect();
                }, 100);
            })
            .catch(err => console.error("Vacancy list error", err));

        // Load Jobs
        vm.loadJobRequirement();
    };

    // ------------------------------------------
    // LOAD JOB REQUIREMENT (Right side listing)
    // ------------------------------------------
    vm.loadJobRequirement = function () {
        vm.isLoading = true;

        careerFactory.getJobRequirementList()
            .then(res => {
                vm.objCareerList = res.data;
            })
            .catch(err => console.error("Job Requirement Error", err))
            .finally(() => vm.isLoading = false);
    };

    // ------------------------------------------
    // APPLY JOB CLICK
    // ------------------------------------------
    vm.applyJob = function (id, vacancyType) {
        var user = localStorage.getItem("User");
        var role = localStorage.getItem("Role");
        var userId = localStorage.getItem("UserId");

        vm.JobName = vacancyType;

        if (!user || user.trim() === "") {
            alert("Please register");
            return;
        }

        if (role === "Admin") {
            alert("Admin can't apply job");
            return;
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

    // ------------------------------------------
    // SAVE APPLY JOB (Validation + API Call)
    // ------------------------------------------
    vm.saveApplyJob = function () {
        vm.isLoading = true;

        const file = vm.objCareer.resume;
        const primary = vm.objCareer.primarySkill;
        const years = vm.objCareer.experienceYear;
        const months = vm.objCareer.experienceMonth;

        // VALIDATION
        if (!primary || primary.trim() === "") {
            alert("Primary Skill is required.");
            vm.isLoading = false;
            return;
        }

        if (primary.length < 3) {
            alert("Primary Skill must be at least 3 characters.");
            vm.isLoading = false;
            return;
        }

        const skillRegex = /^[A-Za-z\s\.\,]+$/;
        if (!skillRegex.test(primary)) {
            alert("Primary Skill should contain only letters.");
            vm.isLoading = false;
            return;
        }

        if (years < 0 || years > 50 || months < 0 || months > 11) {
            alert("Experience not valid.");
            vm.isLoading = false;
            return;
        }

        if (!file) {
            alert("Please upload a PDF file before applying.");
            vm.isLoading = false;
            return;
        }

        if (file.name.split('.').pop().toLowerCase() !== 'pdf') {
            alert("Only PDF files allowed.");
            vm.isLoading = false;
            return;
        }

        // 2MB Limit
        if (file.size > 2 * 1024 * 1024) {
            alert("File size exceeds 2 MB!");
            vm.isLoading = false;
            return;
        }

        // API CALL
        careerFactory.saveApplyJob(vm.objCareer)
            .then(res => {
                if (res.data.success) {
                    alert("Apply Successfully !!!");
                    bootstrap.Modal.getInstance(document.getElementById('careerModal')).hide();
                } else {
                    alert(res.data.message || "Something went wrong");
                }
            })
            .catch(err => console.error("Internal server error", err))
            .finally(() => vm.isLoading = false);
    };

    // ------------------------------------------
    // MODAL INITIALIZATION
    // ------------------------------------------
    setTimeout(() => {
        modal = new bootstrap.Modal(document.getElementById("careerModal"));
    }, 500);
});
