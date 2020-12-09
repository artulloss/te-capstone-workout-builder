import axios from "axios";

export default {
  getFocuses: () => {
    return axios.get("/focus");
  },
};
