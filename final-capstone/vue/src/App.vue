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

/* Scrollbar (Deprecated) */
@media screen and (-webkit-min-device-pixel-ratio: 0) {
  body::-webkit-scrollbar-thumb {
    background-color: rgba(186, 186, 255, 0.5); /* color of the scroll thumb */
    border-radius: 0.2rem; /* roundness of the scroll thumb */
  }
}
*::-webkit-scrollbar,
*::-webkit-scrollbar-thumb {
  width: 16px;
  border-radius: 13px;
  background-clip: padding-box;
  border: 6px solid transparent;
}

body {
  background-color: #333;
}
</style>
