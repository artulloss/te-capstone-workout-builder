import axios from "axios";

export default {
  getTrainerExercise(name, filter = []) {
    let route = `/user/${name}/trainer/exercise`;
    route += this.getQueryRoute(filter);
    console.log("getQueryRoute",this.getQueryRoute(filter))
    return axios.get(route);
  },
  postExercise(exercise) {
    let response = axios.post("/exercise", exercise);
    console.log(response);
    return response;
  },
  getQueryRoute(filter) {
    console.log(filter)
    let query = "";
    for (const [key, param] of filter.entries()) {
      if (key === 0) {
        for (const prop in param) {
          query += `?${prop}=${param[prop]}`;
        }
      } else {
        for (const prop in param) {
          query += `&${prop}=${param[prop]}`;
        }
      }
    }
    console.log(query);
    return query;
  },
};
