app.controller("Register", function (registerFactory) {

    var vm = this;
    var objRegister = {};
    vm.init = function () {

        vm.objRegister = {
            fullName: "",
            username: "",
            password: "",
            email: "",
            mobileNo: "",

        };
    };

    vm.register = function () {
        const fullName = vm.objRegister.fullName;
        const username = vm.objRegister.username;
        const password = vm.objRegister.password;
        const email = vm.objRegister.email;
        const mobileNo = vm.objRegister.mobileNo;

        if (!fullName || fullName.trim().length < 3) {
            alert("Full Name must be at least 3 characters.");
            return false;
        }
        const nameRegex = /^[A-Za-z\s]+$/;
        if (!nameRegex.test(fullName)) {
            alert("Full Name should contain only letters and spaces.");
            return false;
        }
        if (!email) {
            alert("Please enter email.");
            return false;
        }
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email)) {
            alert("Invalid email format.");
            return false;
        }

        if (!username || username.trim().length < 5) {
            alert("Username must be at least 5 characters.");
            return false;
        }
        if (!mobileNo) {
            alert("Please enter mobile number.");
            return false;
        }
        const mobileRegex = /^[0-9]{10,15}$/;
        if (!mobileRegex.test(mobileNo)) {
            alert("Mobile number must be 10–15 digits.");
            return false;
        }
        const usernameRegex = /^[A-Za-z0-9]+$/;
        if (!usernameRegex.test(username)) {
            alert("Username should contain only letters and numbers (no spaces).");
            return false;
        }
        if (!password) {
            alert("Please enter password.");
            return false;
        }
        if (!password || password.length < 4) {
            alert("Password must be at least 5 characters.");
            return false;
        }

        registerFactory.register(vm.objRegister)
            .then(res => {
                alert("Register Successfully !!!");
                window.location.href = "/Home/Login";
            })
            .catch(err => console.error("Issue in register api", err));
    };

    vm.init();
});