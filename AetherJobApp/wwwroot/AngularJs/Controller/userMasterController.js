app.controller("User", function (userFactory) {

    var vm = this;
    var objLogin = {};
    var objRegister = {};
    var listUsers = [];
    vm.username = "";
    vm.role = "";
    vm.init = function () {

        vm.objRegister = {
            fullName: "",
            username: "",
            password: "",
            email: "",
            mobileNo: ""
        };

        vm.objLogin = {
            username: "",
            password: ""
        };
    };
    
    vm.loadUsers = function () {
        userFactory.getAll()
            .then(res => {
                vm.listUsers = res.data;

                setTimeout(() => {
                    if ($.fn.DataTable && !$.fn.DataTable.isDataTable('#tblUsers')) {
                        $('#tblUsers').DataTable({
                            pageLength: 10,
                            lengthMenu: [5, 10, 25, 50],
                            order: [[0, 'desc']]
                            //ordering: true
                        });
                    }
                }, 100);

            })
            .catch(err => console.error("Get User Api issue", err));
    };

    vm.changeStatus = function (id, activeStatus) {
        userFactory.updateActive(id, activeStatus)
            .then(res => {
                vm.loadUsers();
            })
            .catch(err => console.error("Update error", err));
    };
    vm.init();
});