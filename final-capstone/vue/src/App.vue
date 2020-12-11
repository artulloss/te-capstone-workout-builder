<template>
  <v-app>
    <nav-bar />
    <v-main>
      <router-view />

      <!--<div id="app">
        <vue-particles color="#dedede"></vue-particles>
      </div>

      <div id="app">
       <vue-particles
          color="#000000"
          :particleOpacity="0.2"
          :particlesNumber="80"
          shapeType="circle"
          :particleSize="7"
          linesColor="#000000"
          :linesWidth="1"
          :lineLinked="true"
          :lineOpacity="1"
          :linesDistance="150"
          :moveSpeed="3"
        >        
        </vue-particles>

      </div>-->
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
