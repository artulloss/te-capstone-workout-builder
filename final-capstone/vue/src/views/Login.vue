<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <h1 class="display-1">Login</h1>
    </v-card-title>
    <v-card-text>
      <v-form class="form-signin" @submit.prevent="login">
        <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
          Invalid username or password!
        </div>
        <div
          class="alert alert-success"
          role="alert"
          v-if="this.$route.params.registration"
        >
          Thank you for registering, please sign in.
        </div>

        <v-text-field
          id="username"
          outlined
          prepend-icon="mdi-account-circle"
          class="form-control"
          v-model="user.username"
          :rules="usernameRules"
          :counter="50"
          label="Username"
          required
          autofocus
        ></v-text-field>
        <v-text-field
          id="password"
          outlined
          class="form-control"
          prepend-icon="mdi-lock"
          v-model="user.password"
          :rules="passwordRules"
          :counter="50"
          :type="show ? 'text' : 'password'"
          label="Password"
          required
          :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
          @click:append="show = !show"
        ></v-text-field>
        <v-divider />
        <v-card-actions>
          <v-btn color="success" :to="{ name: 'register' }"
            ><span>Register</span></v-btn
          >
          <v-spacer />
          <v-btn color="info" type="submit"><span>Sign in</span></v-btn>
        </v-card-actions>
      </v-form>
    </v-card-text>
  </v-card>
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
            const user = response.data.user;
            this.$store.commit("SET_USER", user);
            if (user.role === "admin") {
              this.$router.push({ name: "admin-exercises" });
              return;
            }
            this.$router.push({ name: "exercises" });
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
