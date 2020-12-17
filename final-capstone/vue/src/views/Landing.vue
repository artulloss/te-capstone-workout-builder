<template>
  <v-container fluid>
    <div class="main-container">
      <v-card class="container mx-auto mt-5">
        <h1>Fitness Elevator</h1>
        <div class="flex-container">
          <v-card-text>
            <p>
              Do you enjoy exercise but struggle to come up with your own
              workout plan on your own? Do your workouts get boring from doing
              the same 5 exercises over and over every day? Then this is the app
              for you. Trainers will be able to input exercises for cardio,
              legs, arms, abs, and back. Users will be able to login, select
              their focus and generate a random workout routine. Exercise has
              never been so exciting!
            </p>
          </v-card-text>

          <div class="btn-container flex-container">
            <v-btn
              v-if="$store.state.token === ''"
              color="blue"
              :to="{ name: 'register' }"
              dark
            >
              <span> <h2>Get Started</h2> </span>
            </v-btn>
            <v-btn
              v-if="
                $store.state.token !== '' && $store.state.user.role == 'admin'
              "
              color="blue"
              :to="{ name: 'admin-exercises' }"
              dark
            >
              <span> <h2>Manage Exercises</h2> </span>
            </v-btn>
            <v-btn
              v-if="
                $store.state.token !== '' && $store.state.user.role == 'user'
              "
              color="blue"
              :to="{ name: 'exercises' }"
              dark
            >
              <span> <h2>My Exercises</h2> </span>
            </v-btn>
          </div>
        </div>
      </v-card>
    </div>
  </v-container>
</template>

<script>
export default {};
</script>

<style scoped>
h1 {
  text-align: center;
  margin-bottom: 0.5rem;
}
.flex-container {
  height: 100%;
  justify-content: flex-start;
  flex-direction: column;
  margin: 0 auto;
  box-sizing: border-box;
}
.btn-container {
  width: fit-content;
}
.main-container {
  width: 100%;
}
h1 {
  font-size: 2.5rem;
}
@media only screen and (max-width: 1000px) {
  .container {
    width: 438px;
  }
}
</style>
