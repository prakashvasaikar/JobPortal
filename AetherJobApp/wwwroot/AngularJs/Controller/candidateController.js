app.controller("Candidate", function (candidateFactory) {

    var vm = this;
    var objCandidate = {};
    var objCandidateList = [];
    var modal;
    vm.isLoading = false;

    vm.fullName = "";
    vm.id = 0;
    vm.vacancyType = "";
    vm.designation = "";
    vm.experience = "";
    vm.referBy = "";

    vm.init = function () {
        vm.objCandidate = {
            id: 0,
            status: "",
            reviewBy: localStorage.getItem("UserId"),
        };
    };
    vm.load = function () {
        vm.isLoading = true;
        candidateFactory.getCandidateList()
            .then(res => {
                vm.objCandidateList = res.data;

                setTimeout(() => {
                    if ($.fn.DataTable && !$.fn.DataTable.isDataTable('#tblCandidate')) {
                        $('#tblCandidate').DataTable({
                            pageLength: 10,
                            lengthMenu: [5, 10, 25, 50],
                            order: [[0, 'desc']],
                            //ordering: false,
                            scrollX: true,          // 👈 enable horizontal scroll
                            scrollCollapse: true,
                            autoWidth: false
                        });
                    }
                }, 200);
            })
            .catch(err => console.error("Vacancy list error", err))
            .finally(() => vm.isLoading = false);
    };

    vm.downloadResume = function (fileName) {
        vm.isLoading = true;
        candidateFactory.downloadResume(fileName)
            .then(function (res) {
                var blob = new Blob([res.data], { type: 'application/pdf' });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = fileName;
                link.click();
                window.URL.revokeObjectURL(link.href);
            })
            .catch(err => console.error("Internal Server error", err))
            .finally(() => vm.isLoading = false);
    };

    vm.updateStatus = function (id, status, fullname, type, designation, year, month, referby) {
        debugger
        vm.fullName = fullname;
        vm.vacancyType = type;
        vm.designation = designation;
        vm.experience = year + "." + month;
        vm.objCandidate.id = id;
        vm.referBy = (referby == "" || referby == null) ? "-" : referby;
        modal.show();
    };

    vm.saveUpdateStatus = function () {
        vm.isLoading = true;
        candidateFactory.updateStatus(vm.objCandidate).then(res => {
            debugger
            alert("Status updated successfully !!!")
            vm.load();
            bootstrap.Modal.getInstance(document.getElementById('CandidateModal')).hide();
        }).catch(err => console.error("Internal server error", err))
            .finally(() => vm.isLoading = false);
    };

    setTimeout(() => {
        modal = new bootstrap.Modal(document.getElementById("CandidateModal"));
    }, 500);

    vm.load();
    vm.init();
});
