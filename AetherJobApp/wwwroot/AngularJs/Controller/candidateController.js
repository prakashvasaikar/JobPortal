app.controller("Candidate", function (candidateFactory) {

    var vm = this;
    var objCandidate = {};
    var objCandidateList = [];

    vm.init = function () {
        vm.objCandidate = {
            id:0,
            refId_CompanyRequirement: 0,
            refId_UserMaster: 0,
            primarySkill: "",
            status: "",
            resume: "",
            referBy: "",
            reviewBy: 0,
            reviewDate: null
        };
    };

    vm.load = function () {
        candidateFactory.getCandidateList().
            then(res => vm.objCandidateList = res.data).
            catch(err => console.error("Issue in candidatelist api", err));
    }

    vm.edit = function () {
        candidateFactory.getCandidateInfoById(vm.objCandidate.id).
            then(res => vm.objCandidate = res.data).
            catch(err => console.error("issue in get api", err));
    }

    vm.save = function () {
        candidateFactory.saveCandidate(vm.objCandidate).
            then(res => {
                alert("Save Successfully !!!");
            }).
            catch(err => console.error("issue in save", err));
    }

    vm.init();
    vm.load();
});