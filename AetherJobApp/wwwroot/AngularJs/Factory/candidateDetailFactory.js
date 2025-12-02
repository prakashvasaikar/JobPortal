app.factory("candidateFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getCandidateList: () => $http.get(baseUrl + "getCandidateList"),
        getCandidateInfoById: id => $http.get(baseUrl + "getCandidateInfoById" + id),
        saveCandidate: objCandidateDetail => $http.post(baseUrl + "saveCandidate", objCandidateDetail),
        deleteById: id => $http.delete(baseUrl + "deleteCandidate" + id),
        downloadResume: pdfName => $http.get(baseUrl + "download" + pdfName, { responseType: 'arraybuffer' }),
        //downloadResume: pdfName => {
        //    return $http.get(baseUrl + "download/" + pdfName, { responseType: 'arraybuffer' });
        //}

    };
});