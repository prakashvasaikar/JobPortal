app.controller("companyRequirementFactory", function (companyRequirementFactory) {

    var vm = this;
    var objCompanyRequirement = {};
    var objCompanyRequirementList = [];

    vm.init = function () {
        vm.objCandidate = {
            id: 0,
            refId_VacancyMaster: 0,
            jobMode: "",
            description: "",
            experience: "",
            createdBy: "",
            createdOn: null
        };
    };

    vm.load = function () {
        companyRequirementFactory.getCompanyRequirementList().
            then(res => vm.objCompanyRequirementList = res.data).
            catch(err => console.error("Issue in companyRequirementlist api", err));
    }

    vm.edit = function () {
        companyRequirementFactory.getCompanyRequirementInfoById(vm.objCompanyRequirement.id).
            then(res => vm.objCompanyRequirement = res.data).
            catch(err => console.error("issue in get api", err));
    }

    vm.save = function () {
        companyRequirementFactory.saveCompanyRequirement(vm.objCompanyRequirement).
            then(res => {
                alert("Save Successfully !!!");
            }).
            catch(err => console.error("issue in save", err));
    }

    vm.init();
    vm.load();
});