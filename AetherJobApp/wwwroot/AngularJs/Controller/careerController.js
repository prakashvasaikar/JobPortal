app.controller("Career", function (careerFactory, toastr) {
    var vm = this;

    vm.objCareerList = [];

    vm.loadJobRequirement = function () {
        careerFactory.getJobRequirementList()
            .then(res => {
                vm.objCareerList = res.data;
                toastr.success("Jobs loaded successfully");
            })
            .catch(err => {
                console.error("jobRequirement list error", err);
                toastr.error("Failed to load jobs");
            });
    };

    vm.applyJob = function (id) {
        var user = localStorage.getItem("User");

        if (!user || user.trim() === "") {
            toastr.warning("Please register to apply");
            return;
        }

        toastr.success("Job Applied Successfully!");
    };

    vm.loadJobRequirement();
});
