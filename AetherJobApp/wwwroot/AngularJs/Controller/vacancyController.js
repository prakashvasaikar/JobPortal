app.controller("Vacancy", function (vacancyFactory) {

    var vm = this;
    vm.objVacancy = {};
    vm.objVacancyList = [];
    vm.isEdit = false;
    var modal;  
    
    vm.init = function () {
        vm.objVacancy = {
            id: 0,
            vacancyType: "",
            isActive: true,
            createdBy: localStorage.getItem("UserId"),
        };
    };

    vm.loadVacancy = function () {
        debugger
        vacancyFactory.getVacancyList()
            .then(res => {
                vm.objVacancyList = res.data;

                setTimeout(() => {
                    if ($.fn.DataTable && !$.fn.DataTable.isDataTable('#tblVacancy')) {
                        $('#tblVacancy').DataTable({
                            pageLength: 10,
                            lengthMenu: [5, 10, 25, 50],
                            ordering: true
                        });
                    }
                }, 100);
            })
            .catch(err => console.error("Vacancy list error", err));
    };

    vm.deleteVacancy = function (id) {
        if (confirm("Are you sure you want to delete?")) {
            vacancyFactory.deleteVacancyById(id)
                .then(res => {
                    //alert("Deleted successfully!!");
                    vm.loadVacancy();
                })
                .catch(err => console.error("Vacancy list error", err));
        }
    };

    vm.openAddModal = function () {
        vm.isEdit = false;

        vm.objVacancy = {
            id: 0,
            vacancyType: "",
            status: "open",
            postedOn: new Date(),
            expiredOn: new Date(),
            isActive: true,
            createdBy: 1
        };

        modal.show();
    };

    vm.openEditModal = function (id) {
        vacancyFactory.getVacancyInfoById(id)
            .then(res => {
                debugger
                vm.objVacancy = res.data;
                vm.objVacancy.postedOn = vm.objVacancy.postedOn ? new Date(vm.objVacancy.postedOn) : null;
                vm.objVacancy.expiredOn = vm.objVacancy.expiredOn ? new Date(vm.objVacancy.expiredOn) : null;
                modal.show();
            })
            .catch(err => console.error("Get vacancy error", err));
    };

    vm.saveVacancy = function () {
        vm.objVacancy.createdBy = localStorage.getItem("UserId");

        if (!vm.objVacancy.vacancyType || vm.objVacancy.vacancyType.trim() === "") {
            alert("Vacancy Type is required.");
            return;
        }

        vacancyFactory.saveVacancy(vm.objVacancy)
            .then(res => {
                alert("Saved Successfully!");
                bootstrap.Modal.getInstance(document.getElementById('vacancyModal')).hide();
                vm.loadVacancy();
            })
            .catch(err => console.error("Save vacancy error", err));
    };

   
    vm.changeStatus = function (id, status) {
        debugger
        vacancyFactory.updateActive(id, !status)
            .then(() => vm.loadVacancy())
            .catch(err => console.error("Status update error", err));
    };

   
    setTimeout(() => {
        modal = new bootstrap.Modal(document.getElementById("vacancyModal"));
    }, 500);

    vm.init();

});
