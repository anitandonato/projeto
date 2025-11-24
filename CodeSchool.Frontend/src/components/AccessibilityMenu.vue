<template>
    <div class="accessibility-menu">
        <button @click="mostrarMenu = !mostrarMenu" class="btn-accessibility" aria-label="Abrir menu de acessibilidade"
            :aria-expanded="mostrarMenu">
            ♿ Acessibilidade
        </button>

        <div v-if="mostrarMenu" class="menu-painel" role="dialog" aria-label="Menu de Acessibilidade">
            <div class="menu-header">
                <h3>♿ Acessibilidade</h3>
                <button @click="mostrarMenu = false" class="btn-fechar" aria-label="Fechar menu">✕</button>
            </div>

            <div class="menu-opcoes">
                <!-- ALTO CONTRASTE -->
                <div class="opcao">
                    <label class="switch">
                        <input type="checkbox" :checked="accessibilityStore.altoContraste"
                            @change="accessibilityStore.toggleAltoContraste()"
                            aria-label="Ativar modo alto contraste" />
                        <span class="slider"></span>
                    </label>
                    <span class="opcao-label">Alto Contraste</span>
                </div>

                <!-- TAMANHO DA FONTE -->
                <div class="opcao">
                    <span class="opcao-label">Tamanho da Fonte:</span>
                    <div class="fonte-controles">
                        <button @click="accessibilityStore.setTamanhoFonte('pequeno')"
                            :class="{ ativo: accessibilityStore.tamanhFonte === 'pequeno' }" aria-label="Fonte pequena">
                            A
                        </button>
                        <button @click="accessibilityStore.setTamanhoFonte('normal')"
                            :class="{ ativo: accessibilityStore.tamanhFonte === 'normal' }" aria-label="Fonte normal"
                            style="font-size: 1.2rem">
                            A
                        </button>
                        <button @click="accessibilityStore.setTamanhoFonte('grande')"
                            :class="{ ativo: accessibilityStore.tamanhFonte === 'grande' }" aria-label="Fonte grande"
                            style="font-size: 1.4rem">
                            A
                        </button>
                    </div>
                </div>

                <!-- NARRAÇÃO -->
                <div class="opcao">
                    <label class="switch">
                        <input type="checkbox" :checked="accessibilityStore.narracao"
                            @change="accessibilityStore.toggleNarracao()" aria-label="Ativar narração de texto" />
                        <span class="slider"></span>
                    </label>
                    <span class="opcao-label">Narração de Áudio</span>
                </div>

                <!-- REDUÇÃO DE MOVIMENTO -->
                <div class="opcao">
                    <label class="switch">
                        <input type="checkbox" :checked="accessibilityStore.reducaoMovimento"
                            @change="accessibilityStore.toggleReducaoMovimento()"
                            aria-label="Reduzir animações e movimentos" />
                        <span class="slider"></span>
                    </label>
                    <span class="opcao-label">Reduzir Movimentos</span>
                </div>
            </div>

            <div class="menu-footer">
                <p>Atalhos de Teclado:</p>
                <ul>
                    <li><kbd>Alt</kbd> + <kbd>1</kbd>: Ir para conteúdo principal</li>
                    <li><kbd>Alt</kbd> + <kbd>C</kbd>: Alto contraste</li>
                    <li><kbd>Alt</kbd> + <kbd>+</kbd>: Aumentar fonte</li>
                    <li><kbd>Alt</kbd> + <kbd>-</kbd>: Diminuir fonte</li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import { useAccessibilityStore } from '../stores/accessibility'

const accessibilityStore = useAccessibilityStore()
const mostrarMenu = ref(false)
</script>

<style scoped>
.accessibility-menu {
    position: fixed;
    top: 80px;
    right: 20px;
    z-index: 9999;
}

.btn-accessibility {
    padding: 12px 20px;
    background: #667eea;
    color: white;
    border: none;
    border-radius: 25px;
    font-weight: bold;
    cursor: pointer;
    box-shadow: 0 4px 15px rgba(102, 126, 234, 0.4);
    transition: all 0.3s;
}

.btn-accessibility:hover {
    background: #5568d3;
    transform: translateY(-2px);
    box-shadow: 0 6px 20px rgba(102, 126, 234, 0.5);
}

.menu-painel {
    position: absolute;
    top: 60px;
    right: 0;
    background: white;
    border-radius: 15px;
    padding: 25px;
    box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
    min-width: 350px;
    max-width: 400px;
}

.menu-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
    padding-bottom: 15px;
    border-bottom: 2px solid #e0e0e0;
}

.menu-header h3 {
    margin: 0;
    color: #667eea;
}

.btn-fechar {
    background: none;
    border: none;
    font-size: 1.5rem;
    cursor: pointer;
    color: #666;
    padding: 5px 10px;
}

.btn-fechar:hover {
    color: #ff6b6b;
}

.menu-opcoes {
    display: flex;
    flex-direction: column;
    gap: 20px;
}

.opcao {
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 15px;
    padding: 10px;
    border-radius: 8px;
    transition: background 0.3s;
}

.opcao:has(input:checked) {
    background: #e3f2fd;
}

.opcao-label {
    font-weight: 600;
    color: #333;
}

/* SWITCH TOGGLE */
.switch {
    position: relative;
    display: inline-block;
    width: 50px;
    height: 24px;
}

.switch input {
    opacity: 0;
    width: 0;
    height: 0;
}

.slider {
    position: absolute;
    cursor: pointer;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: #ccc;
    transition: 0.4s;
    border-radius: 24px;
}

.slider:before {
    position: absolute;
    content: "";
    height: 18px;
    width: 18px;
    left: 3px;
    bottom: 3px;
    background-color: white;
    transition: 0.4s;
    border-radius: 50%;
}

input:checked+.slider {
    background-color: #667eea;
}

input:checked+.slider:before {
    transform: translateX(26px);
}

/* CONTROLES DE FONTE */
.fonte-controles {
    display: flex;
    gap: 8px;
}

.fonte-controles button {
    padding: 8px 12px;
    background: #f0f0f0;
    border: 2px solid #e0e0e0;
    border-radius: 8px;
    cursor: pointer;
    font-weight: bold;
    transition: all 0.3s;
}

.fonte-controles button:hover {
    background: #e0e0e0;
}

.fonte-controles button.ativo {
    background: #667eea;
    color: white;
    border-color: #667eea;
}

.menu-footer {
    margin-top: 20px;
    padding-top: 15px;
    border-top: 2px solid #e0e0e0;
}

.menu-footer p {
    font-weight: 600;
    color: #667eea;
    margin-bottom: 10px;
}

.menu-footer ul {
    list-style: none;
    padding: 0;
    margin: 0;
}

.menu-footer li {
    padding: 5px 0;
    font-size: 0.9rem;
    color: #666;
}

kbd {
    background: #f0f0f0;
    border: 1px solid #ccc;
    border-radius: 3px;
    padding: 2px 6px;
    font-family: monospace;
    font-size: 0.85rem;
}

/* ALTO CONTRASTE */
html.alto-contraste .menu-painel {
    background: #000000 !important;
    border: 3px solid #FFFFFF !important;
}

html.alto-contraste .menu-header h3,
html.alto-contraste .opcao-label,
html.alto-contraste .menu-footer p,
html.alto-contraste .menu-footer li {
    color: #FFFFFF !important;
}

html.alto-contraste .btn-fechar {
    color: #FFFFFF !important;
}

html.alto-contraste .fonte-controles button {
    background: #000000 !important;
    color: #FFFFFF !important;
    border-color: #FFFFFF !important;
}

html.alto-contraste .fonte-controles button.ativo {
    background: #FFD700 !important;
    color: #000000 !important;
}

html.alto-contraste .opcao:has(input:checked) {
    background: #1a1a1a !important;
    border: 2px solid #FFD700 !important;
}
</style>