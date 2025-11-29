app.controller("Vacancy", function (vacancyFactory) {

    var vm = this;
    var objVacancy = {};
    var objVacancyList = [];

    vm.init = function () {
        vm.objVacancy = {
            id:0,
            vacancyType: "",
            status: "",
            postedOn: null,
            expiredOn: null,
            isActive: null,
            createdBy: 0,
            createdOn: null,
            updatedOn: null
        };
    };

    vm.load = function () {
        vacancyFactory.getVacancyList().
            then(res => vm.objVacancyList = res.data).
            catch(err => console.error("Issue in Vacancylist api", err));
    }

    vm.edit = function () {
        vacancyFactory.getVacancyInfoById(vm.objVacancy.id).
            then(res => vm.objVacancy = res.data).
            catch(err => console.error("issue in get api", err));
    }

    vm.save = function () {
        vacancyFactory.saveVacancy(vm.objVacancy).
            then(res => {
                alert("Save Successfully !!!");
            }).
            catch(err => console.error("issue in save", err));
    }

    vm.init();
    vm.load();
});