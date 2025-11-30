app.controller("Register", function (registerFactory) {

    var vm = this;
    var objRegister = {};
    vm.init = function () {

        vm.objRegister = {
            fullName: "",
            username: "",
            password: "",
            email: "",
            mobileNo: ""
        };
    };

    vm.register = function () {
        if (!vm.objRegister.fullName) {
            alert("please enter fullname");
            return false;
        }
        if (!vm.objRegister.username) {
            alert("please enter username");
            return false;
        }
        if (!vm.objRegister.password) {
            alert("please enter password");
            return false;
        }
        if (!vm.objRegister.email) {
            alert("please enter email");
            return false;
        }
        if (!vm.objRegister.mobileNo) {
            alert("please enter mobile no");
            return false;
        }
        registerFactory.register(vm.objRegister)
            .then(res => {
                alert("Register Successfully !!!")
                window.location.href = "/Home/Login";
            })
            .catch(err => console.error("Issue in register api", err));
    };

    vm.init();
});