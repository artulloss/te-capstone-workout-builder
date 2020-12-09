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


export default {
  name: "exercise-list",
  data() {
    return {
      exerciseVisibility: [],
    };
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
  props: {
    exercises: Array,
  },
};
</script>

<style></style>
