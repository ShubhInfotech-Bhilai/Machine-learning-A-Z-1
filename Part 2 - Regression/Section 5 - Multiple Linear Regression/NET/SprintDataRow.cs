namespace PredictSprintVelocity {
    public class SprintDataRow {
        public int SprintNumber { get; set; }
        public int HoursProgrammer1 { get; set; }
        public int HoursProgrammer2 { get; set; }
        public int HoursProgrammer3 { get; set; }
        public double NumberOfProcessedStoryPoints { get; set; }

        public SprintDataRow() { }

        public SprintDataRow(int sprintNumber, int hoursProgrammer1, int hoursProgrammer2, int hoursProgrammer3) {
            this.SprintNumber = sprintNumber;
            this.HoursProgrammer1 = hoursProgrammer1;
            this.HoursProgrammer2 = hoursProgrammer2;
            this.HoursProgrammer3 = hoursProgrammer3;
        }
    }
}