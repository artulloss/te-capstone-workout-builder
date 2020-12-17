<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <div class="flex-container">
        <h1 class="display-1">Workout History</h1>
        <v-btn :to="{ name: 'exercises' }" color="primary">Exercises</v-btn>
      </div>
    </v-card-title>
    <v-card-text>
      <workout-line-chart :chartData="chartData" :options="chartOptions" />
    </v-card-text>
    <v-text-field
      id="days"
      outlined
      class="form-control"
      :rules="numericRules"
      label="Days"
      v-model.number="days"
      append-outer-icon="mdi-chevron-up"
      prepend-icon="mdi-chevron-down"
      @click:append-outer="days++"
      @click:prepend="days--"
    />
  </v-card>
</template>

<script>
import WorkoutLineChart from "@/components/WorkoutLineChart.vue";
import axios from "axios";
import utilities from "@/utilities";
const localStorage = window.localStorage;

export default {
  name: "workout-history",
  data() {
    return {
      response: {},
      chartData: {
        labels: [],
        datasets: [
          {
            label: "Minutes",
            backgroundColor: "#f87979",
            data: [],
          },
        ],
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
      },
      days: 0,
      numericRules: utilities.numericRules,
    };
  },
  created() {
    axios
      .get(`/workoutHistory/${this.$store.state.user.userId}`)
      .then((response) => {
        if (response.status === 200) {
          this.reponse = response;
          let daysString = localStorage.getItem("days");
          if (daysString === null) {
            this.days = 7; // Default to 1 week
            return;
          }
          this.days = Number(daysString);
        }
      });
  },
  components: { WorkoutLineChart },
  watch: {
    days(newDays, oldDays) {
      if (newDays === oldDays) {
        return;
      }
      let startDate = new Date();
      startDate.setDate(startDate.getDate() - newDays + 1);
      const dateStrings = this.getDateStrings(
        startDate, // From today - length + 1 days
        new Date() // To today
      );
      const responseData = this.reponse.data;
      const dates = responseData.map((wH) => new Date(wH.date).toDateString());
      const initialTimes = responseData.map((wH) => wH.time / 60); // Minutes
      const dataset = [];
      for (const dateString of dateStrings) {
        let notZero = false;
        for (const [index, date] of dates.entries()) {
          if (dateString === new Date(date).toDateString()) {
            dataset.push(initialTimes[index]);
            notZero = true;
          }
        }
        if (notZero) continue;
        dataset.push(0);
      }
      this.setChartData(dateStrings, dataset);
      localStorage.setItem("days", newDays); // Save to local storage :p
    },
  },
  methods: {
    setChartData(labels, dataset) {
      const chartData = { ...this.chartData };
      chartData.labels = labels;
      chartData.datasets[0].data = dataset;
      this.chartData = chartData;
    },
    getDateStrings(dt, end) {
      const dates = [];
      while (dt <= end) {
        dates.push(new Date(dt));
        dt.setDate(dt.getDate() + 1);
      }
      //console.log(dates);
      return dates.map((d) => d.toDateString());
    },
  },
};
</script>

<style scoped>
.flex-container {
  justify-content: space-between;
  width: 100%;
}
</style>
