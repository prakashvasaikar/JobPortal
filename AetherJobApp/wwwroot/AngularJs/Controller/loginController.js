app.controller("login", function (userFactory) {

    var vm = this;
    var objLogin = {};
    vm.init = function () {

        vm.objLogin = {
            username: "",
            password: ""
        };
    };

    vm.login = function () {
        if (!vm.objLogin.username && !vm.objLogin.password) {
            alert("Please check username or password");
            return false;
        }
        userFactory.login(vm.objLogin)
            .then(res => {
                localStorage.setItem("UserId", res.data.userId);
                localStorage.setItem("User", res.data.login);
                localStorage.setItem("Role", res.data.roles);
                window.location.href = "/Home/Index";
            })
            .catch(err => {
                if (err.data && err.data.message) {
                    alert(err.data.message);
                } else {
                    alert("Login failed. Please try again.");
                }
            });

    };

    vm.init();


});