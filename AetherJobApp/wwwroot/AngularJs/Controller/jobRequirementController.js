app.controller("JobRequirement", function (jobRequirementFactory, vacancyFactory) {

    var vm = this;
    vm.objjobRequirement = {};
    vm.objjobRequirementList = [];
    vm.isEdit = false;
    var modal;

    vm.init = function () {
        vm.objjobRequirement = {
            id: 0,
            refId_VacancyMaster: 0,
            status: "Open",
            postedOn: null,
            expiredOn: null,
            isActive: true,
            jobMode: "",
            jobDescription: "",
            createdBy: localStorage.getItem("UserId"),
        };
       
        vacancyFactory.getActiveVacancyList()
            .then(res => {
                vm.objVacancyList = res.data;
            })
            .catch(err => console.error("Vacancy list error", err));

    };

    vm.loadJobRequirement = function () {
        jobRequirementFactory.getJobRequirementList()
            .then(res => {
                vm.objjobRequirementList = res.data;

                setTimeout(() => {
                    if ($.fn.DataTable && !$.fn.DataTable.isDataTable('#tblJobRequirement')) {
                        $('#tblJobRequirement').DataTable({
                            pageLength: 10,
                            lengthMenu: [5, 10, 25, 50],
                            ordering: true
                        });
                    }
                }, 100);
            })
            .catch(err => console.error("jobRequirement list error", err));
    };

    vm.deletejobRequirement = function (id) {
        if (confirm("Are you sure you want to delete?")) {
            jobRequirementFactory.deletejobRequirementById(id)
                .then(res => {
                    vm.loadJobRequirement();
                })
                .catch(err => console.error("jobRequirement list error", err));
        }
    };

    vm.openAddModal = function () {
        vm.isEdit = false;

        vm.objjobRequirement = {
            id: 0,
            refId_VacancyMaster: 0,
            status: "Open",
            postedOn: new Date(),
            expiredOn: new Date(),
            isActive: true,
            createdBy: 1
        };

        modal.show();
    };

    vm.openEditModal = function (id) {
        jobRequirementFactory.getJobRequirementInfoById(id)
            .then(res => {
                debugger
                vm.objjobRequirement = res.data;
                vm.objjobRequirement.postedOn = vm.objjobRequirement.postedOn ? new Date(vm.objjobRequirement.postedOn) : null;
                vm.objjobRequirement.expiredOn = vm.objjobRequirement.expiredOn ? new Date(vm.objjobRequirement.expiredOn) : null;
                modal.show();
            })
            .catch(err => console.error("Get jobRequirement error", err));
    };

    vm.saveJobRequirement = function () {
        debugger
        vm.objjobRequirement.createdBy = localStorage.getItem("UserId");


        if (vm.objjobRequirement.refId_VacancyMaster == null || vm.objjobRequirement.refId_VacancyMaster === 0) {
            alert("Vacancy Type is required.");
            return;
        }

        if (!vm.objjobRequirement.status || vm.objjobRequirement.status.trim() === "") {
            alert("Status is required.");
            return;
        }

        if (!vm.objjobRequirement.postedOn) {
            alert("Posted On date is required.");
            return;
        }

        if (!vm.objjobRequirement.expiredOn) {
            alert("Expired On date is required.");
            return;
        }

        if (new Date(vm.objjobRequirement.expiredOn) <= new Date(vm.objjobRequirement.postedOn)) {
            alert("Expired On must be later than Posted On.");
            return;
        }

        if (vm.objjobRequirement.createdBy == null || vm.objjobRequirement.createdBy === "") {
            alert("Created By is required.");
            return;
        }

        jobRequirementFactory.saveJobRequirement(vm.objjobRequirement)
            .then(res => {
                alert("Saved Successfully!");
                bootstrap.Modal.getInstance(document.getElementById('JobRequirementModal')).hide();
                vm.loadJobRequirement();
            })
            .catch(err => console.error("Save jobRequirement error", err));
    };

    vm.changeStatus = function (id, status) {
        jobRequirementFactory.updateActive(id, !status)
            .then(() => vm.loadJobRequirement())
            .catch(err => console.error("Status update error", err));
    };

    setTimeout(() => {
        modal = new bootstrap.Modal(document.getElementById("JobRequirementModal"));
    }, 500);

    vm.init();

});
