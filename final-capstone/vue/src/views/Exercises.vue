<template>
  <div>
    <h1>Manage Exercises</h1>
    <v-combobox
      id="focus"
      outlined
      class="form-control"
      :rules="focusRules"
      label="Focus"
      :items="focusNames"
      v-model="pickedFocusName"
      required
    />
    <v-text-field
      id="time"
      suffix="seconds"
      outlined
      label="Time"
      v-model.number="exercise.time"
      required
      class="form-control"
      :rules="requiredNumericRules"
      append-outer-icon="mdi-chevron-up"
      prepend-icon="mdi-chevron-down"
      @click:append-outer="exercise.time += 10"
      @click:prepend="exercise.time -= 10"
    />
    <exercise-list />
  </div>
</template>

<script>
import ExerciseList from "@/components/ExerciseList";
export default {
  components: { ExerciseList },
};
</script>

<style></style>
