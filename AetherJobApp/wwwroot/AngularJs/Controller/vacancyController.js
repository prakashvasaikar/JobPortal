app.controller("Vacancy", function ($scope, vacancyFactory) {

    var vm = this;
    vm.objVacancy = {};
    vm.objVacancyList = [];
    vm.isEdit = false;
    var modal;  
    
    vm.init = function () {
        vm.objVacancy = {
            id: 0,
            vacancyType: "",
            status: "open",
            postedOn: null,
            expiredOn: null,
            isActive: true,
            createdBy: 1
        };
    };

    vm.loadVacancy = function () {

        vacancyFactory.getVacancyList()
            .then(res => {
                vm.objVacancyList = res.data;

                setTimeout(() => {
                    if ($.fn.DataTable.isDataTable("#tblVacancy")) {
                        $('#tblVacancy').DataTable().destroy();
                    }

                    $('#tblVacancy').DataTable({
                        pageLength: 10,
                        lengthMenu: [5, 10, 25, 50],
                        ordering: true
                    });

                }, 200);
            })
            .catch(err => console.error("Vacancy list error", err));
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

    // -----------------------------
    // OPEN EDIT MODAL
    // -----------------------------
    //vm.openEditModal = function (id) {
    //    vm.isEdit = true;

    //    vacancyFactory.getVacancyInfoById(id)
    //        .then(res => {
    //            vm.objVacancy = res.data;

    //            // Format dates for input[type=date]
    //            vm.objVacancy.postedOn = vm.objVacancy.postedOn?.split("T")[0];
    //            vm.objVacancy.expiredOn = vm.objVacancy.expiredOn?.split("T")[0];

    //            modal.show();
    //        })
    //        .catch(err => console.error("Get vacancy error", err));
    //};

    // -----------------------------
    // SAVE VACANCY (ADD/EDIT)
    // -----------------------------
    vm.saveVacancy = function () {

        vacancyFactory.saveVacancy(vm.objVacancy)
            .then(res => {
                alert("Saved Successfully !");
                modal.hide();
                vm.loadVacancy();
            })
            .catch(err => console.error("Save vacancy error", err));
    };

    // -----------------------------
    // CHANGE ACTIVE / INACTIVE
    // -----------------------------
    vm.changeStatus = function (id, status) {
        vacancyFactory.updateStatus(id, !status)
            .then(() => vm.loadVacancy())
            .catch(err => console.error("Status update error", err));
    };

    // -----------------------------
    // INITIALIZE BOOTSTRAP MODAL
    // -----------------------------
    setTimeout(() => {
        modal = new bootstrap.Modal(document.getElementById("vacancyModal"));
    }, 500);

    // CALL INIT
    vm.init();
    vm.loadVacancy();

});
