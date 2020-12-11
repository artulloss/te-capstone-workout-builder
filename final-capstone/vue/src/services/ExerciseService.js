import axios from "axios";

export default {
  getTrainerExercise(name, filter = {}) {
    let route = `/user/${name}/trainer/exercise`;
    route += this.getQueryRoute(filter);
    console.log("getQueryRoute", this.getQueryRoute(filter));
    return axios.get(route);
  },
  postExercise(exercise) {
    let response = axios.post("/exercise", exercise);
    console.log(response);
    return response;
  },
  updateExercise(exercise) {
    let response = axios.put("/exercise", exercise);
    console.log(response);
    return response;
  },
  // In {time: 10, focusId: 1} etc
  getQueryRoute(filter) {
    let query = "";
    let first = true;
    for (const prop in filter) {
      if (first) {
        query += `?${prop}=${filter[prop]}`;
        first = false;
      } else {
        query += `&${prop}=${filter[prop]}`;
      }
    }
    return query;
  },
};
