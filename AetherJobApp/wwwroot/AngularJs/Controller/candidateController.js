app.controller("Candidate", function (candidateFactory) {

    var vm = this;
    var objCandidate = {};
    var objCandidateList = [];

    //vm.init = function () {
    //    vm.objCandidate = {
    //        id:0,
    //        refId_CompanyRequirement: 0,
    //        refId_UserMaster: 0,
    //        primarySkill: "",
    //        status: "",
    //        resume: "",
    //        referBy: "",
    //        reviewBy: 0,
    //        reviewDate: null
    //    };
    //};

    
    vm.load = function () {
        debugger
        candidateFactory.getCandidateList()
            .then(res => {
                vm.objCandidateList = res.data;

                setTimeout(() => {
                    if ($.fn.DataTable && !$.fn.DataTable.isDataTable('#tblCandidate')) {
                        $('#tblCandidate').DataTable({
                            pageLength: 10,
                            lengthMenu: [5, 10, 25, 50],
                            ordering: true,
                            scrollX: true,          // 👈 enable horizontal scroll
                            scrollCollapse: true,
                            autoWidth: false
                        });
                    }
                }, 200);
            })
            .catch(err => console.error("Vacancy list error", err));
    };
    vm.downloadResume = function (fileName) {
        debugger
        candidateFactory.downloadResume(fileName)
            .then(function (res) {
                debugger
                var blob = new Blob([res.data], { type: 'application/pdf' });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = fileName;
                link.click();
                window.URL.revokeObjectURL(link.href);
            })
            .catch(err => console.error("Internal Server error", err));
    };


    //vm.edit = function () {
    //    candidateFactory.getCandidateInfoById(vm.objCandidate.id).
    //        then(res => vm.objCandidate = res.data).
    //        catch(err => console.error("issue in get api", err));
    //}

    //vm.save = function () {
    //    candidateFactory.saveCandidate(vm.objCandidate).
    //        then(res => {
    //            alert("Save Successfully !!!");
    //        }).
    //        catch(err => console.error("issue in save", err));
    //}
    vm.load();
});
