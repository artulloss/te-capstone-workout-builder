import axios from "axios";

export default {
  getTrainerExercise: (name) => {
    return axios.get(`/user/${name}/trainer/exercise`);
  },
};
