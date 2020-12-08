import axios from "axios";

export default {
  getTrainerExercise: (name) => {
    return axios.get(`/user/${name}/trainer/exercise`);
  },
  postExercise: (exercise) => {
    return axios.post("/exercise", exercise);
  }
};
