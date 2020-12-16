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
export default {
  name: "workout-history",
  data() {
    return {
      chartDataInternal: {
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
      days: 7,
      numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],
    };
  },
  created() {
    axios
      .get(`/workoutHistory/${this.$store.state.user.userId}`)
      .then((response) => {
        if (response.status === 200) {
          const chartDataInternal = { ...this.chartDataInternal };
          chartDataInternal.labels = this.getLabels(response.data);
          chartDataInternal.datasets[0].data = this.getData(
            response.data,
            chartDataInternal.labels
          );
          this.chartDataInternal = chartDataInternal;
        }
      });
  },
  components: { WorkoutLineChart },
  /*watch: {
    days(newDays, oldDays) {
      if (newDays === oldDays) {
        return;
      }
      let date = new Date();
      date.setDate(date.getDate() - newDays + 1);
      const labels = this.getLabels([
        {
          date,
        },
        {
          date: new Date(),
        },
      ]);
      console.log(labels);
      let placeholder = this.chartDataInternal.datasets.data.
      const data = this.getData(placeholder, labels);
      console.log(data);
    },
  },*/
  computed: {
    chartData() {
      return this.chartDataInternal;
    },
  },
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
        }
        if (notZero) continue;
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
