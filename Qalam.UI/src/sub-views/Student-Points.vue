<template>
    <v-container>
        <v-row align="center">
            <p class="primary--text"><span class="points">800</span>نقطة</p>
        </v-row>

        <custom-card class="v-card-profile text-right" color="primary" :title="$locales.t('howToUsePoints')" customClass="white--text px-5 py-3">
            <v-container>
                <v-row no-gutters>
                    <p>
                        في البداية هذا أول تعامل لي معكم ولن يكون الأخير بإذن الله صراحة قمة في الذوق وقمة في الإحترام والأهم النتيجة النهائية للعمل أكثر من رائعة وهذا هو الشيء المهم سلاسة في التعامل وصبر على كثرة الملاحظات والتعديلات كلمة حق تستحقون كل خير اتمنى لكم التوفيق
                    </p>
                </v-row>
                <v-row no-gutters>
                    <p>
                        في البداية هذا أول تعامل لي معكم ولن يكون الأخير بإذن الله صراحة قمة في الذوق وقمة في الإحترام والأهم النتيجة النهائية للعمل أكثر من رائعة وهذا هو الشيء المهم سلاسة في التعامل وصبر على كثرة الملاحظات والتعديلات كلمة حق تستحقون كل خير اتمنى لكم التوفيق
                    </p>
                </v-row>
                <v-row no-gutters>
                    <p>
                        في البداية هذا أول تعامل لي معكم ولن يكون الأخير بإذن الله صراحة قمة في الذوق وقمة في الإحترام والأهم النتيجة النهائية للعمل أكثر من رائعة وهذا هو الشيء المهم سلاسة في التعامل وصبر على كثرة الملاحظات والتعديلات كلمة حق تستحقون كل خير اتمنى لكم التوفيق
                    </p>
                </v-row>
                <v-row no-gutters>
                    <p>
                        في البداية هذا أول تعامل لي معكم ولن يكون الأخير بإذن الله صراحة قمة في الذوق وقمة في الإحترام والأهم النتيجة النهائية للعمل أكثر من رائعة وهذا هو الشيء المهم سلاسة في التعامل وصبر على كثرة الملاحظات والتعديلات كلمة حق تستحقون كل خير اتمنى لكم التوفيق
                    </p>
                </v-row>
            </v-container>
        </custom-card>
    </v-container>
</template>

<script>
import CustomCard from '@/components/cards/custom-card.vue'

export default {
    name: "StudentPoints",

    components: {
        CustomCard
    }
}
</script>

<style scoped>
.points{
    font-size: 150px;
}
</style>