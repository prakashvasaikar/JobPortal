app.controller("User", function (userFactory) {

    var vm = this;
    var objLogin = {};
    var objRegister = {};
    vm.username = "";

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

    (function () {
        debugger
        vm.username = localStorage.getItem("User");
        if (localStorage.getItem("Role") == "User") {
            document.querySelector('.clsJobRequirement').style.display = 'none';
        }
        else {
            document.querySelector('.clsJobRequirement').style.display = 'block';
        }
    })();

    vm.login = function () {
        if (!vm.objLogin.username && !vm.objLogin.password) {
            alert("Please check username or password");
            return false;
        }
        userFactory.login(vm.objLogin)
            .then(res => {
                debugger
                localStorage.setItem("User", res.data.login);
                localStorage.setItem("Role", res.data.roles);
                vm.username = res.data.login;
                window.location.href = "/Home/Index";
            })
            .catch(err => console.error("Issue in login api", err));
    };

    vm.logout = function () {
        localStorage.clear();
        window.location.href = "/Home/Login";
    }

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
        userFactory.register(vm.objRegister)
            .then(res => {
                alert("Register Successfully !!!")
                window.location.href = "/Home/Login";
            })
            .catch(err => console.error("Issue in register api", err));
    }

    vm.init();
});