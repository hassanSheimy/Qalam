<template>
    <v-card class="justify-center">
        <v-card-title class="border-bottom">
            {{title}}
        </v-card-title>
        <v-card-text class="rating-data">
            <v-container>
                <v-row no-gutters class="ma-3">
                    <v-col sm="3" cols="12">
                        <v-icon color="yellow darken-3">fas fa-star</v-icon>
                        نجمة واحدة
                    </v-col>
                    <v-col sm="9" cols="12">
                        <v-progress-linear
                            color="red darken-2"
                            rounded
                            :value="data[0]"
                            striped
                            height="20">
                        </v-progress-linear>
                    </v-col>
                </v-row>
                <v-row no-gutters class="ma-3">
                    <v-col sm="3" cols="12">
                        <v-icon color="yellow darken-3">fas fa-star</v-icon>
                        نجمتان
                    </v-col>
                    <v-col sm="9" cols="12">
                    <v-progress-linear
                        color="indigo"
                        rounded
                        :value="data[1]"
                        striped
                        height="20">
                    </v-progress-linear>
                    </v-col>
                </v-row>
                <v-row no-gutters class="ma-3">
                    <v-col sm="3" cols="12">
                        <v-icon color="yellow darken-3">fas fa-star</v-icon>
                        ثلاث نجمات
                    </v-col>
                    <v-col sm="9" cols="12">
                    <v-progress-linear
                        color="teal"
                        rounded
                        :value="data[2]"
                        striped
                        height="20">
                    </v-progress-linear>
                    </v-col>
                </v-row>
                <v-row no-gutters class="ma-3">
                    <v-col sm="3" cols="12">
                        <v-icon color="yellow darken-3">fas fa-star</v-icon>
                        اربع نجمات
                    </v-col>
                    <v-col sm="9" cols="12">
                        <v-progress-linear
                            color="info darken-2"
                            rounded
                            :value="data[3]"
                            striped
                            height="20">
                        </v-progress-linear>
                    </v-col>
                </v-row>
                <v-row no-gutters class="ma-3">
                    <v-col sm="3" cols="12">
                        <v-icon color="yellow darken-3">fas fa-star</v-icon>
                        خمس نجمات
                    </v-col>
                    <v-col sm="9" cols="12">
                        <v-progress-linear
                            color="primary darken-2"
                            rounded
                            :value="data[4]"
                            striped
                            height="20">
                        </v-progress-linear>
                    </v-col>
                </v-row>
                <v-row no-gutters class="ma-3">
                    <v-col sm="3" cols="12">
                        متوسط التقيمات
                    </v-col>
                    <v-col sm="9" cols="12">
                        <v-progress-linear
                            color="cyan darken-2"
                            rounded
                            :value="averagePercent"
                            striped
                            height="20">
                        </v-progress-linear>
                    </v-col>
                </v-row>
            </v-container>
        </v-card-text>
    </v-card>
</template>

<script>
export default {
    name: "RatingComponent",
    props: {
        title: {
            type: String,
            required: true
        },
        data: {
            type: Array,
            required: true
        }
    },
    computed: {
        average(){
            var { data } = this;
            return ((data[4] * 5) + (data[3] * 4) + (data[2] * 3) + (data[1] * 2) + data[0]) / 100.0;
        },
        averagePercent(){
            return (this.average / 5) * 100;
        }
    }
}
</script>

<style scoped>
.rating-data{
    height: 272px;
    overflow: auto;
}
</style>