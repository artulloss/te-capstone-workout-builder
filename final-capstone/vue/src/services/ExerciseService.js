import axios from "axios";

export default {
  getTrainerExercises(name, filter = {}) {
    let route = `/user/${name}/trainer/exercise`;
    route += this.getQueryRoute(filter);
    console.log("getQueryRoute", this.getQueryRoute(filter));
    return axios.get(route);
  },
  getUserExercises(username) {
    return axios.get(`/user/${username}/exercise`);
  },
  postExercise(exercise) {
    let response = axios.post("/exercise", exercise);
    console.log(response);
    return response;
  },
  updateExercise(exercise) {
    // This route should probably be made to be /exercise/{id}
    let response = axios.put("/exercise", exercise);
    console.log(response);
    return response;
  },
  deleteExercise(exerciseId) {
    let response = axios.delete(`/exercise/${exerciseId}`);
    console.log(response);
    return response;
  },
  addUserExercises(username, exercises) {
    return axios.post(`/user/${username}/exercise`, exercises);
  },
  deleteUserExercises(username) {
    return axios.delete(`/user/${username}/exercise`);
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
