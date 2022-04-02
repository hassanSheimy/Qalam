<template>
    <v-card v-bind="$attrs">
        <v-card-title class="border-bottom">
            {{title}}
        </v-card-title>
        <v-card-text class="opinion-data">
            <v-list class="overflow-y-auto">
                <v-list-item v-for="i in 10" :key="i">
                    <v-col cols="3">
                        January 29, 2016
                    </v-col>
                    <v-col cols="9">
                        <v-rating
                            v-model="rating"
                            :readonly="true"
                            color="yellow darken-3">
                        </v-rating>
                        <div class="review-block-title">this was nice in buy</div>
                        <div class="review-block-description">this was nice in buy. this was nice in buy. this was nice in buy. this was nice in buy this was nice in buy this was nice in buy this was nice in buy this was nice in buy</div>
                    </v-col>
                </v-list-item>
            </v-list>
        </v-card-text>
    </v-card>
</template>

<script>
export default {
    name: "UserOpinion",
    
    props: {
        title: {
            type: String,
            required: true
        },
    },

    data: () => ({
        rating: 3
    })
}
</script>

<style scoped>
.opinion-data{
    height: 272px;
    overflow-y: scroll
}
</style>