<template>
  <div class="flex-container" @change="onChange">
    <v-text-field
      id="exercise-name"
      outlined
      class="form-control"
      v-model="exerciseInternal.exerciseName"
      :rules="exerciseNameRules"
      :counter="50"
      label="Name"
      required
      autofocus
    ></v-text-field>
    <v-textarea
      id="description"
      outlined
      class="form-control"
      v-model="exerciseInternal.description"
      :counter="2000"
      label="Description"
      required
    ></v-textarea>
    <v-select
      id="focus"
      outlined
      class="form-control"
      label="Focus"
      :items="focusNames"
      v-model="pickedFocusName"
      required
    />
    <div class="flex-container flex-row">
      <v-text-field
        id="time"
        suffix="s"
        outlined
        label="Time"
        v-model.number="exerciseInternal.time"
        required
        class="form-control"
        :rules="requiredNumericRules"
        append-outer-icon="mdi-chevron-up"
        prepend-icon="mdi-chevron-down"
        @click:append-outer="changeExercisePropValueBy('time', 10)"
        @click:prepend="changeExercisePropValueBy('time', -10)"
      />
      <v-text-field
        id="weight"
        suffix="lbs"
        outlined
        class="form-control"
        :rules="numericRules"
        label="Weight"
        v-model.number="exerciseInternal.weight"
        append-outer-icon="mdi-chevron-up"
        prepend-icon="mdi-chevron-down"
        @click:append-outer="changeExercisePropValueBy('weight', 1)"
        @click:prepend="changeExercisePropValueBy('weight', -1)"
      />
    </div>
    <div class="flex-container flex-row">
      <v-text-field
        id="repetitions"
        outlined
        class="form-control"
        :rules="numericRules"
        label="Repetitions"
        v-model.number="exerciseInternal.repetitions"
        append-outer-icon="mdi-chevron-up"
        prepend-icon="mdi-chevron-down"
        @click:append-outer="changeExercisePropValueBy('repetitions', 1)"
        @click:prepend="changeExercisePropValueBy('repetitions', -1)"
      />
      <v-text-field
        id="sets"
        outlined
        class="form-control"
        :rules="numericRules"
        label="Sets"
        v-model.number="exerciseInternal.sets"
        append-outer-icon="mdi-chevron-up"
        prepend-icon="mdi-chevron-down"
        @click:append-outer="changeExercisePropValueBy('sets', 1)"
        @click:prepend="changeExercisePropValueBy('sets', -1)"
      />
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      exerciseInternal: { ...this.exercise }, // shallow clone (same props)
      pickedFocusName: "",
      exerciseNameRules: [
        (v) =>
          (v || "").length <= 50 || "A maximum of 50 characters is allowed",
      ],
      numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],
    };
  },
  props: {
    exercise: {
      // TODO Make most props on other components required
      type: Object,
      required: true,
    },
    focuses: {
      type: Array,
      required: true,
    },
  },
  computed: {
    focusNames() {
      return this.focuses.map(
        (f) => f.focusName.charAt(0).toUpperCase() + f.focusName.slice(1) // Capitalize first letter :p
      );
    },
    requiredNumericRules() {
      return (
        this.numericRules.concat((v) => (v || "").length !== 0) ||
        "This is a required field"
      );
    },
  },
  methods: {
    onChange() {
      console.log("CHANGED");
      this.fixExercise();
      this.$emit("edit-exercise", this.exerciseInternal);
    },
    fixExercise() {
      for (const prop in this.exerciseInternal) {
        if (["exerciseName", "description", "time"].includes(prop)) break;
        if (this.exerciseInternal[prop] === "") {
          this.exerciseInternal[prop] = null;
        }
      }
    },
    changeExercisePropValueBy(prop, amount) {
      let newValue = Number(this.exerciseInternal[prop]) + amount;
      this.exerciseInternal[prop] = newValue === 0 ? null : newValue;
      this.onChange();
    },
  },
};
</script>

<style scoped>
.flex-container {
  flex-direction: column;
}
.flex-row {
  flex-direction: row;
  justify-content: space-evenly;
}
</style>
