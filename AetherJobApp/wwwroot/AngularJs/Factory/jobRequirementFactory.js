app.factory("jobRequirementFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getJobRequirementList: () => $http.get(baseUrl + "getJobRequirementList"),
        getJobRequirementInfoById: id => $http.get(baseUrl + "getJobRequirementInfoById/" + id),
        saveJobRequirement: objjobRequirement => $http.post(baseUrl + "saveJobRequirement", objjobRequirement),
        updateActive: (id, isActive) =>
            $http.post(baseUrl + "updateJobRequirementActive", { id: id, isActive: isActive }),
        deletejobRequirementById: id => $http.delete(baseUrl + "deleteJobRequirement/" + id),
    };
});