<template>
  <v-app>
    <nav-bar />
    <v-main>
      <router-view />
      <particles-bg
        type="circle"
        :canvas="{ backgroundColor: '#888' }"        
        :bg="true"        
      /> 
    </v-main>
  </v-app>
</template>

<script>
import NavBar from "@/components/NavBar.vue";
import { ParticlesBg } from "particles-bg-vue";

export default {
  name: "App",

  components: {
    NavBar,
    ParticlesBg,
  },
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
.v-main {
  z-index: 1;
}
</style>
