export default {
  numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],
  requiredNumericRules() {
    return (
      this.numericRules.concat((v) => (v || "").length !== 0) ||
      "This is a required field"
    );
  },
  capitalizeFirstLetter(string) {
    string = string + "";
    return string.charAt(0).toUpperCase() + string.slice(1);
  },
};
