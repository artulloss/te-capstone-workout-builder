<template>
  <v-app-bar app color="purple" dark>
    <div class="d-flex align-center">
      <router-link :to="{ name: 'landing' }">
        <v-img
          alt="Fitness Elevator"
          class="shrink mr-2"
          contain
          src="@/assets/logo.png"
          transition="scale-transition"
          width="40"
        />
      </router-link>

      <h2>Fitness Elevator</h2>
    </div>

    <v-spacer></v-spacer>
    <v-btn text v-if="$store.state.token !== ''" @click="logout">
      <span class="mr-2">Logout</span>
      <v-icon>mdi-logout</v-icon>
    </v-btn>

    <v-btn :to="{ name: 'login' }" text v-else>
      <span class="mr-2">Login</span>
      <v-icon>mdi-login</v-icon>
    </v-btn>
  </v-app-bar>
</template>

<script>
export default {
  name: "nav-bar",
  methods: {
    logout() {
      this.$store.commit("LOGOUT");
      this.$router.push({ name: "landing" });
    },
  },
};
</script>

<style></style>
