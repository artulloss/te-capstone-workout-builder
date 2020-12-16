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
  </v-card>
</template>

<script>
import WorkoutLineChart from "@/components/WorkoutLineChart.vue";
import axios from "axios";
export default {
  name: "workout-history",
  data() {
    return {
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
    };
  },
  created() {
    axios
      .get(`/workoutHistory/${this.$store.state.user.userId}`)
      .then((response) => {
        if (response.status === 200) {
          const chartData = { ...this.chartData };
          const datasets = [...this.chartData.datasets];
          chartData.labels = this.getLabels(response.data);
          datasets[0].data = this.getData(response.data, chartData.labels);
          console.log(chartData);
          this.chartData = chartData;
        }
      });
  },
  components: { WorkoutLineChart },
  methods: {
    getData(data, labels) {
      const timesListed = data.map((wH) => wH.time / 60);
      const datesListed = data.map((wH) => new Date(wH.date));
      data = [];
      for (const dateString of labels) {
        let notZero = false;
        for (const [index, date] of datesListed.entries()) {
          if (date.toDateString() === dateString) {
            data.push(timesListed[index]);
            notZero = true;
            break;
          }
          console.log("date.toDateString() !== dateString");
        }
        if (notZero) continue;
        console.log("PUSHED ZERO", dateString);
        data.push(0);
      }
      return data;
    },
    getLabels(data) {
      const datesListed = data.map((wH) => new Date(wH.date));
      const dates = [];
      if (datesListed[0] === undefined) return []; // If no data at all
      const dt = datesListed[0];
      const end = datesListed[datesListed.length - 1];
      while (dt <= end) {
        dates.push(new Date(dt));
        dt.setDate(dt.getDate() + 1);
      }
      console.log(dates);
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
