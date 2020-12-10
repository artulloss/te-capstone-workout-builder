<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <h1 class="display-1">Create Account</h1>
    </v-card-title>
    <v-card-text>
      <v-form class="form-signin" @submit.prevent="register">
        <div
          class="alert alert-danger"
          role="alert"
          v-if="registrationErrorMsg"
        >
          {{ this.registrationErrorMsg }}
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
          outlined
          prepend-icon="mdi-account-circle"
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
          outlined
          class="form-control"
          prepend-icon="mdi-lock"
          v-model="user.password"
          :rules="usernameRules"
          :counter="50"
          :type="show1 ? 'text' : 'password'"
          label="Password"
          required
          :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
          @click:append="show1 = !show1"
        ></v-text-field>

        <v-text-field
          id="confirm-password"
          class="form-control"
          :rules="confirmPasswordRules"
          outlined
          prepend-icon="mdi-lock"
          v-model="user.confirmPassword"
          :passwordRules="passwordRules"
          :counter="50"
          :type="show2 ? 'text' : 'password'"
          label="Confirm Password"
          required
          :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
          @click:append="show2 = !show2"
        ></v-text-field>

        <v-divider />
        <v-card-actions>
          <v-btn color="info" :to="{ name: 'login' }"
            ><span>Have an account?</span></v-btn
          >
          <v-spacer />
          <v-btn color="success" type="submit"><span>Register</span></v-btn>
        </v-card-actions>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "register",
  data() {
    return {
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
      },
      registrationErrorMsg: undefined,
      show1: false,
      show2: false,
      usernameRules: [
        (v) =>
          (v || "").length <= 50 || `A maximum of 50 characters is allowed`,
        (v) => (v || "").indexOf(" ") < 0 || "No spaces are allowed",
      ],
      passwordRules: [() => true],
      confirmPasswordRules: [
        () =>
          this.user.password === this.user.confirmPassword ||
          this.user.confirmPassword === "" ||
          "Passwords do not match",
      ],
    };
  },
  methods: {
    register() {
      if (!this.user.username) {
        this.registrationErrorMsg = "You must provide a username";
      } else if (!this.user.password) {
        this.registrationErrorMsg = "You must provide a password";
      } else if (!this.user.confirmPassword) {
        this.registrationErrorMsg = "You must confirm your password";
      } else if (this.user.password !== this.user.confirmPassword) {
        this.registrationErrorMsg = "Passwords do not match";
      } else {
        this.registrationErrorMsg = undefined;
        authService
          .register(this.user)
          .then((response) => {
            if (response.status === 201) {
              this.$router.push({
                name: "login",
                params: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            if (response.status === 409) {
              this.registrationErrorMsg = "Bad Request: Account already exists";
            } else if (response.status > 400 && response.status < 500) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            } else {
              console.log(response);
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style></style>
