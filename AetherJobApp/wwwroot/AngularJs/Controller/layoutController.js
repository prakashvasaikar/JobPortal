app.controller("Layout", function ($scope) {

    var vm = this;   // 🔥 correct place

    vm.username = "";
    vm.role = "";

    vm.init = function () {
        vm.username = localStorage.getItem("User");
        vm.role = localStorage.getItem("Role");
    };

    vm.logout = function () {
        localStorage.clear();
        window.location.href = "/Home/Login";
    };

});
