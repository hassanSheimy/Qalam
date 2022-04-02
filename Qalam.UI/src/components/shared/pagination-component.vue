<template>
    <div class="text-center">
        <v-container>
            <v-row justify="center">
                <v-col cols="12">
                    <v-container class="max-width">
                        <v-pagination
                            @next="NextPage"
                            @previous="PrevPage"
                            @input="ChangePage"
                            v-model="page"
                            class="my-4"
                            :length="length"
                            :total-visible="totalVisible"
                            circle>
                        </v-pagination>
                    </v-container>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
export default {
    name: "PaginationComponent",
    props: {
        length: {
            type: Number,
            default: 10
        },
        totalVisible: {
            type: Number,
            default: 10
        }
    },
    
    data: () => ({
        page: 1
    }),

    created() {
        if (!this.$listeners.changePage) {
            throw 'Missing required event listener: "changePage"';
        }
    },

    methods: {
        NextPage() {
        },
        PrevPage() {
        },
        ChangePage(page){
        }
    }
}
</script>

<style scoped>
</style>