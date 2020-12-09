import axios from "axios";

export default {
  getTrainerExercise: (name) => {
    return axios.get(`/user/${name}/trainer/exercise`);
  },
  postExercise: (exercise) => {
    let response = axios.post("/exercise", exercise);
    console.log(response);
    return response;
  },
};
