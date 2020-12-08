<template>
  <div id="login" class="text-center">
    <v-form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >
        Thank you for registering, please sign in.
      </div>
      <v-text-field
        id="username"
        class="form-control"
        v-model="user.username"
        :usernameRules="usernameRules"
        :counter="50"
        label="Username"
        required
        autofocus
      ></v-text-field>
      <v-text-field
        id="password"
        class="form-control"
        v-model="user.password"
        :usernameRules="usernameRules"
        :counter="50"
        :type="show ? 'text' : 'password'"
        label="Password"
        required
        :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
        @click:append="show = !show"
      ></v-text-field>
      <v-btn :to="{ name: 'register' }"><span>Register</span></v-btn>
      <v-spacer />
      <v-btn type="submit"><span>Sign in</span></v-btn>
    </v-form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
      usernameRules: [
        (v) =>
          (v || "").length <= 50 || `A maximum of 50 characters is allowed`,
        (v) => (v || "").indexOf(" ") < 0 || "No spaces are allowed",
      ],
      passwordRules: [() => true], // TODO Eventually use regex to validate password strength
      // https://stackoverflow.com/questions/5142103/regex-to-validate-password-strength
      show: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch((error) => {
          const response = error.response;
          if (response === undefined) {
            console.log("No response returned from server");
            return;
          }
          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>
