app.factory("companyRequirementFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getCompanyRequirementList: () => $http.get(baseUrl + "getCompanyRequirementList"),
        getCompanyRequirementInfoById: id => $http.get(baseUrl + "getCompanyRequirementInfoById" + id),
        saveCompanyRequirement: objCandidateDetail => $http.post(baseUrl + "saveCompanyRequirement", objCandidateDetail),
    };
});