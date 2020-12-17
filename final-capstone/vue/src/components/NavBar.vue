<template>
  <v-app-bar app dark>
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
      <h2 class="big">Fitness Elevator</h2>
      <h3 class="small">Fitness Elevator</h3>
      <div class="flex-container">
        <p id="slogan"><em>"Taking you to the next level of fitness"</em></p>
      </div>
    </div>

    <v-spacer />
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

<style scoped>
.flex-container {
  margin-left: 2.5rem;
  align-items: center;
  height: 100%;
}
#slogan {
  margin-bottom: 0;
}
.small {
  display: none;
}
@media only screen and (max-width: 454px) {
  #slogan {
    display: none;
  }
  .big {
    display: none;
  }
  .small {
    display: inline;
  }
}
</style>
