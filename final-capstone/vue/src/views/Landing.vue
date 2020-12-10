<template>
  <v-container fluid>
    <div class="main-container">
      <h1>Fitness Elevator</h1>
      <div class="flex-container">
        <p>
          Do you enjoy exercise but struggle to come up with your own workout
          plan on your own? Do your workouts get boring from doing the same 5
          exercises over and over every day? Then this is the app for you.
          Trainers will be able to input exercises for cardio, legs, arms, abs,
          and back. Users will be able to login, select their focus and and
          generate a random workout routine. Exercise has never been so
          exciting!
        </p>
        <div class="btn-container flex-container">
          <v-btn color="blue" :to="{name: 'register'}" dark>
            <span> <h2>Get Started</h2> </span>
          </v-btn>
        </div>
      </div>
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
  width: 65%;
  display: flex;
  justify-content: flex-start;
  flex-direction: column;
  margin: 0 auto;
  box-sizing: border-box;
}
.btn-container {
  width: fit-content;
}
.main-container {
  padding: 5rem 2rem;
}
</style>
