app.controller("Career", function (careerFactory) {
    var vm = this;

    vm.objCareerList = [];

    vm.loadJobRequirement = function () {
        careerFactory.getJobRequirementList()
            .then(res => {
                vm.objCareerList = res.data;
            })
            .catch(err => console.error("jobRequirement list error", err));
    };

    vm.applyJob = function (id) {
        debugger
        var user = localStorage.getItem("User");

        if (user === null || user.trim() === "") {
            alert("Please register");
        }

    };

    // Call on page load
    vm.loadJobRequirement();
});
