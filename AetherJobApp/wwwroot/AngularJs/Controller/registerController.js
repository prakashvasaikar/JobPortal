app.controller("Register", function (registerFactory) {

    var vm = this;
    var objRegister = {};
    vm.objcountryList = [];
    vm.objStateList = [];
    vm.objCityList = [];
    vm.isLoading = false;
    vm.showPassword = false;

    vm.init = function () {
        vm.objRegister = {
            fullName: "",
            username: "",
            password: "",
            email: "",
            mobileNo: "",
            refId_CountryMaster: null,
            refId_StateMaster: null,
            refId_CityMaster: null,
        };
    };
    vm.countrylist = function () {

        registerFactory.countryGetlist().
            then(res => vm.objcountryList = res.data).
            catch(err => console.error("Internal server error"));
    }
    vm.onCountryChange = function () {
        vm.objCityList = [];
        registerFactory.stateGetlist(vm.objRegister.refId_CountryMaster).
            then(res => vm.objStateList = res.data).
            catch(err => console.error("Internal server error"));
    };

    vm.onStateChange = function () {
        registerFactory.cityGetlist(vm.objRegister.refId_StateMaster).
            then(res => vm.objCityList = res.data).
            catch(err => console.error("Internal server error"));
    };

    vm.register = function () {
        
        const fullName = vm.objRegister.fullName;
        const username = vm.objRegister.username;
        const password = vm.objRegister.password;
        const email = vm.objRegister.email;
        const mobileNo = vm.objRegister.mobileNo;
        const refId_CountryMaster = vm.objRegister.refId_CountryMaster;
        const refId_StateMaster = vm.objRegister.refId_StateMaster;
        const refId_CityMaster = vm.objRegister.refId_CityMaster;

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
        if (refId_CountryMaster == null || refId_CountryMaster === 0) {
            alert("Country Type is required.");
            return;
        }
        if (refId_StateMaster == null || refId_StateMaster === 0) {
            alert("State Type is required.");
            return;
        }
        if (refId_CityMaster == null || refId_CityMaster === 0) {
            alert("City Type is required.");
            return;
        }
        vm.isLoading = true;
        registerFactory.register(vm.objRegister)
            .then(res => {
                alert("Register Successfully !!!");
                window.location.href = "/Home/Login";
            })
            .catch(err => console.error("Issue in register api", err))
            .finally(() => vm.isLoading = false);
    };

    vm.init();
    vm.countrylist();
});