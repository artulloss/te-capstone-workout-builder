<template>
  <div>
    <div v-for="e in exercises" :key="e.exerciseId">
      <h3 v-on:click="toggleExerciseVisible(e.exerciseId)">
        {{ e.exerciseName }}
      </h3>
      <!-- <div> -->
      <div v-if="exerciseVisibility[e.exerciseId]">
        <p>{{ e.description }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import exerciseService from "@/services/ExerciseService";

export default {
  name: "exercise-list",
  data() {
    return {
      exercises: [],
      exerciseVisibility: [],
    };
  },
  created() {
    exerciseService
      .getTrainerExercise(this.$store.state.user.username)
      .then((response) => {
        if (response.status === 200) {
          console.log(response);
          this.exercises = response.data;
        }
      });
  },
  methods: {
    toggleExerciseVisible(exerciseId) {
      Vue.set(
        this.exerciseVisibility,
        exerciseId,
        !this.exerciseVisibility[exerciseId]
      );
    },
  },
};
</script>

<style></style>
