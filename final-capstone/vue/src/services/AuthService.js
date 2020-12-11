import axios from "axios";
import store from "@/store/index";

export default {
  login(user) {
    return axios.post("/login", user);
  },
  register(user) {
    return axios.post("/register", user);
  },
  getCurrentUser() {
    const config = {
      headers: {
        'Authorization': 'Bearer ' + store.state.token
      }
    }
    return axios.get("/user", config);
  }
};
