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
                debugger
                localStorage.setItem("User", res.data.login);
                localStorage.setItem("Role", res.data.roles);
                window.location.href = "/Home/Index";
            })
            .catch(err => console.error("Issue in login api", err));
    };

    vm.init();


});