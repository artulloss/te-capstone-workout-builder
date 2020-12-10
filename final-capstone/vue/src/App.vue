<template>
  <v-app>
    <nav-bar />
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
import NavBar from "@/components/NavBar.vue";

export default {
  name: "App",

  components: {
    NavBar,
  },

  data: () => ({
    //
  }),
};
</script>

<style>
.container {
  padding: 2rem;
  width: 65%;
}
.flex-container {
  display: flex;
}
.alert {
  margin-bottom: 0.75rem;
}
.alert-danger {
  color: red;
}
.alert-success {
  color: green;
}
</style>
