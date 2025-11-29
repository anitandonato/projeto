<template>
  <div class="recursos">
    <AccessibilityMenu />

    <header class="header">
      <button @click="voltar" class="btn-voltar">← Voltar</button>
      <h1>📚 Recursos Pedagógicos</h1>
    </header>

    <main class="content">
      <div class="intro">
        <h2>Material de Apoio para Professores</h2>
        <p>Aqui você encontra materiais complementares para usar em suas aulas, alinhados com a BNCC e com atividades desplugadas.</p>
      </div>

      <!-- CARD 1: NARRATIVAS -->
      <div class="recurso-card">
        <div class="recurso-header">
          <div class="icone">📖</div>
          <div class="titulo-info">
            <h3>História do Explorador Digital</h3>
            <span class="tag">Narrativas</span>
          </div>
        </div>

        <div class="recurso-conteudo">
          <p><strong>Descrição:</strong> História completa que contextualiza os desafios da plataforma em uma narrativa envolvente.</p>

          <div class="topicos">
            <h4>📌 O que contém:</h4>
            <ul>
              <li>4 capítulos progressivos alinhados aos desafios</li>
              <li>Sistema de recompensas gamificado</li>
              <li>Mensagens motivacionais para alunos</li>
              <li>Glossário de termos de programação</li>
            </ul>
          </div>

          <div class="uso">
            <h4>💡 Como usar:</h4>
            <p>Apresente a narrativa no início do curso para engajar os alunos. Use as mensagens motivacionais quando perceberem dificuldade.</p>
          </div>
        </div>

        <div class="recurso-acoes">
          <button @click="verNarrativas" class="btn-visualizar">👁️ Visualizar</button>
          <button @click="baixarNarrativas" class="btn-download">📥 Baixar PDF</button>
        </div>
      </div>

      <!-- CARD 2: ATIVIDADES DESPLUGADAS -->
      <div class="recurso-card destaque">
        <div class="badge-destaque">⭐ Mais Baixado</div>
        <div class="recurso-header">
          <div class="icone">🎯</div>
          <div class="titulo-info">
            <h3>Banco de Atividades Desplugadas</h3>
            <span class="tag">8 Atividades</span>
          </div>
        </div>

        <div class="recurso-conteudo">
          <p><strong>Descrição:</strong> Atividades offline para ensinar pensamento computacional sem computador.</p>

          <div class="topicos">
            <h4>📌 O que contém:</h4>
            <ul>
              <li><strong>Robô Humano:</strong> Algoritmos e sequência de comandos</li>
              <li><strong>Dança dos Loops:</strong> Compreender repetições</li>
              <li><strong>Árvore de Decisões:</strong> Condicionais (if/else)</li>
              <li><strong>Caixas de Memória:</strong> Compreender variáveis</li>
              <li><strong>Quebra-Cabeça:</strong> Decomposição de problemas</li>
              <li><strong>Pixel Art:</strong> Coordenadas e abstração</li>
              <li><strong>Caça ao Tesouro:</strong> Debug e correção</li>
              <li><strong>Gráfico Humano:</strong> Análise de dados</li>
            </ul>
          </div>

          <div class="uso">
            <h4>💡 Como usar:</h4>
            <p>Ideal para: turmas sem acesso a computadores, introduzir conceitos antes da aula digital, ou reforçar aprendizado com atividades práticas.</p>
          </div>
        </div>

        <div class="recurso-acoes">
          <button @click="verAtividades" class="btn-visualizar">👁️ Visualizar</button>
          <button @click="baixarAtividades" class="btn-download">📥 Baixar PDF</button>
        </div>
      </div>

      <!-- CARD 3: MAPEAMENTO BNCC -->
      <div class="recurso-card">
        <div class="recurso-header">
          <div class="icone">📊</div>
          <div class="titulo-info">
            <h3>Mapeamento BNCC</h3>
            <span class="tag">Alinhamento Curricular</span>
          </div>
        </div>

        <div class="recurso-conteudo">
          <p><strong>Descrição:</strong> Documentação completa do alinhamento da plataforma com a Base Nacional Comum Curricular.</p>

          <div class="topicos">
            <h4>📌 O que contém:</h4>
            <ul>
              <li>5 Competências Gerais da BNCC atendidas</li>
              <li>15+ habilidades de Matemática (6º ao 9º ano)</li>
              <li>100% dos 4 pilares do Pensamento Computacional</li>
              <li>Mapeamento desafio × habilidade BNCC</li>
              <li>Exemplo de relatório pedagógico</li>
              <li>Justificativa para coordenação</li>
            </ul>
          </div>

          <div class="uso">
            <h4>💡 Como usar:</h4>
            <p>Use para justificar o uso da plataforma junto à coordenação pedagógica, planejar aulas alinhadas à BNCC, e elaborar relatórios de aprendizagem.</p>
          </div>
        </div>

        <div class="recurso-acoes">
          <button @click="verBNCC" class="btn-visualizar">👁️ Visualizar</button>
          <button @click="baixarBNCC" class="btn-download">📥 Baixar PDF</button>
        </div>
      </div>

      <!-- DICAS RÁPIDAS -->
      <div class="dicas-section">
        <h2>💡 Dicas de Uso</h2>
        <div class="dicas-grid">
          <div class="dica-card">
            <div class="dica-icone">📝</div>
            <h4>Planejamento</h4>
            <p>Use o mapeamento BNCC para planejar suas aulas e identificar quais desafios trabalham cada habilidade.</p>
          </div>
          <div class="dica-card">
            <div class="dica-icone">🎯</div>
            <h4>Antes da Aula Digital</h4>
            <p>Faça uma atividade desplugada para introduzir o conceito antes dos alunos irem ao computador.</p>
          </div>
          <div class="dica-card">
            <div class="dica-icone">🔄</div>
            <h4>Reforço</h4>
            <p>Alunos com dificuldade? Use atividades físicas para concretizar conceitos abstratos de programação.</p>
          </div>
          <div class="dica-card">
            <div class="dica-icone">📖</div>
            <h4>Motivação</h4>
            <p>Compartilhe a narrativa do Explorador Digital para engajar e contextualizar o aprendizado.</p>
          </div>
        </div>
      </div>

      <!-- MODAL: VISUALIZAR CONTEÚDO -->
      <div v-if="modalVisualizar" class="modal-overlay" @click="fecharModal">
        <div class="modal-grande" @click.stop>
          <div class="modal-header">
            <h2>{{ tituloModal }}</h2>
            <button @click="fecharModal" class="btn-fechar">✕</button>
          </div>
          <div class="modal-conteudo" v-html="conteudoModal"></div>
          <div class="modal-footer">
            <button @click="fecharModal" class="btn-cancelar">Fechar</button>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'

const router = useRouter()
const modalVisualizar = ref(false)
const tituloModal = ref('')
const conteudoModal = ref('')

function voltar() {
  router.push('/professor')
}

function verNarrativas() {
  router.push('/narrativa')
}

function verAtividades() {
  tituloModal.value = '🎯 Atividades Desplugadas'
  conteudoModal.value = `
    <div class="preview-conteudo">
      <p><em>Este é um preview. Baixe o PDF para ver o conteúdo completo com formatação.</em></p>
      <h3>8 Atividades Incluídas:</h3>
      <ol>
        <li><strong>Robô Humano</strong> - Ensinar algoritmos e sequência de comandos usando um grid no chão</li>
        <li><strong>Dança dos Loops</strong> - Compreender loops através de movimentos repetidos</li>
        <li><strong>Árvore de Decisões</strong> - Ensinar condicionais com problemas do dia a dia</li>
        <li><strong>Caixas de Memória</strong> - Compreender variáveis usando caixas físicas</li>
        <li><strong>Quebra-Cabeça de Algoritmos</strong> - Decomposição usando receitas</li>
        <li><strong>Pixel Art com Grid</strong> - Coordenadas e abstração visual</li>
        <li><strong>Caça ao Tesouro Algorítmico</strong> - Debug e correção de erros</li>
        <li><strong>Gráfico Humano</strong> - Coleta e análise de dados</li>
      </ol>
      <p><strong>Cada atividade inclui:</strong> Objetivos, materiais necessários, passo a passo detalhado, competências BNCC, e sugestões de uso.</p>
    </div>
  `
  modalVisualizar.value = true
}

function verBNCC() {
  tituloModal.value = '📊 Mapeamento BNCC'
  conteudoModal.value = `
    <div class="preview-conteudo">
      <p><em>Este é um preview. Baixe o PDF para ver o mapeamento completo.</em></p>
      <h3>✅ Competências Gerais Atendidas (5 de 10):</h3>
      <ul>
        <li><strong>Competência 1:</strong> Conhecimento (Ciências da Computação)</li>
        <li><strong>Competência 2:</strong> Pensamento Científico e Criativo</li>
        <li><strong>Competência 4:</strong> Comunicação (Linguagem de Programação)</li>
        <li><strong>Competência 5:</strong> Cultura Digital</li>
      </ul>

      <h3>📐 Habilidades de Matemática (15+):</h3>
      <ul>
        <li>EF06MA16 - Plano cartesiano e coordenadas</li>
        <li>EF06MA18 - Polígonos e características</li>
        <li>EF07MA18 - Operações com números racionais</li>
        <li>EF08MA06 - Porcentagem e cálculos</li>
        <li>EF09MA06 - Funções e relações</li>
        <li>... e mais 10 habilidades</li>
      </ul>

      <h3>🧠 Pensamento Computacional (100%):</h3>
      <ul>
        <li>✅ Decomposição - Todos os desafios</li>
        <li>✅ Reconhecimento de Padrões - Desafios 3,4,7,9</li>
        <li>✅ Abstração - Todos os desafios</li>
        <li>✅ Algoritmos - Todos os desafios</li>
      </ul>
    </div>
  `
  modalVisualizar.value = true
}

function fecharModal() {
  modalVisualizar.value = false
}

function baixarNarrativas() {
  alert('📥 Gerando PDF de Narrativas...\n\nEm um sistema real, isto baixaria o PDF. Por enquanto, você pode abrir o arquivo narrativas.md na pasta do projeto.')
  // Em produção: gerar PDF ou servir arquivo estático
}

function baixarAtividades() {
  alert('📥 Gerando PDF de Atividades Desplugadas...\n\nEm um sistema real, isto baixaria o PDF. Por enquanto, você pode abrir o arquivo atividades-desplugadas.md na pasta do projeto.')
}

function baixarBNCC() {
  alert('📥 Gerando PDF do Mapeamento BNCC...\n\nEm um sistema real, isto baixaria o PDF. Por enquanto, você pode abrir o arquivo mapeamento-bncc.md na pasta do projeto.')
}
</script>

<style scoped>
.recursos {
  min-height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.header {
  background: white;
  padding: 20px 40px;
  display: flex;
  align-items: center;
  gap: 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.btn-voltar {
  padding: 10px 20px;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
}

.btn-voltar:hover {
  background: #5568d3;
}

.header h1 {
  margin: 0;
  color: #333;
}

.content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 40px 20px;
}

.intro {
  background: white;
  border-radius: 15px;
  padding: 30px;
  margin-bottom: 30px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.intro h2 {
  color: #667eea;
  margin-top: 0;
}

.intro p {
  font-size: 1.1rem;
  color: #666;
  margin-bottom: 0;
}

.recurso-card {
  background: white;
  border-radius: 15px;
  padding: 30px;
  margin-bottom: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
  position: relative;
  transition: transform 0.3s;
}

.recurso-card:hover {
  transform: translateY(-5px);
}

.recurso-card.destaque {
  border: 3px solid #ffd700;
}

.badge-destaque {
  position: absolute;
  top: -12px;
  right: 30px;
  background: linear-gradient(135deg, #ffd700 0%, #ffed4e 100%);
  color: #333;
  padding: 8px 20px;
  border-radius: 20px;
  font-weight: bold;
  font-size: 0.9rem;
  box-shadow: 0 3px 10px rgba(255, 215, 0, 0.4);
}

.recurso-header {
  display: flex;
  align-items: center;
  gap: 20px;
  margin-bottom: 20px;
}

.icone {
  font-size: 4rem;
  flex-shrink: 0;
}

.titulo-info h3 {
  margin: 0 0 8px 0;
  color: #333;
  font-size: 1.8rem;
}

.tag {
  background: #667eea;
  color: white;
  padding: 5px 15px;
  border-radius: 15px;
  font-size: 0.85rem;
  font-weight: 600;
}

.recurso-conteudo p {
  font-size: 1.1rem;
  color: #555;
  line-height: 1.6;
  margin-bottom: 20px;
}

.topicos {
  background: #f8f9fa;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 15px;
}

.topicos h4 {
  color: #667eea;
  margin-top: 0;
}

.topicos ul {
  margin: 10px 0 0 20px;
}

.topicos li {
  margin: 8px 0;
  line-height: 1.5;
}

.uso {
  background: #e3f2fd;
  padding: 15px 20px;
  border-radius: 10px;
  border-left: 4px solid #2196f3;
}

.uso h4 {
  color: #1976d2;
  margin-top: 0;
}

.uso p {
  margin-bottom: 0;
  color: #555;
}

.recurso-acoes {
  display: flex;
  gap: 15px;
  margin-top: 20px;
  padding-top: 20px;
  border-top: 2px solid #eee;
}

.btn-visualizar, .btn-download {
  flex: 1;
  padding: 15px 30px;
  border: none;
  border-radius: 10px;
  font-size: 1.1rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-visualizar {
  background: #667eea;
  color: white;
}

.btn-visualizar:hover {
  background: #5568d3;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
}

.btn-download {
  background: #28a745;
  color: white;
}

.btn-download:hover {
  background: #218838;
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(40, 167, 69, 0.4);
}

.dicas-section {
  background: white;
  border-radius: 15px;
  padding: 30px;
  margin-top: 30px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.dicas-section h2 {
  color: #667eea;
  text-align: center;
  margin-bottom: 25px;
}

.dicas-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 20px;
}

.dica-card {
  background: #f8f9fa;
  padding: 20px;
  border-radius: 12px;
  text-align: center;
  transition: all 0.3s;
}

.dica-card:hover {
  background: #e3f2fd;
  transform: translateY(-3px);
}

.dica-icone {
  font-size: 3rem;
  margin-bottom: 10px;
}

.dica-card h4 {
  color: #667eea;
  margin: 10px 0;
}

.dica-card p {
  font-size: 0.95rem;
  color: #666;
  margin: 0;
  line-height: 1.5;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  padding: 20px;
}

.modal-grande {
  background: white;
  border-radius: 15px;
  max-width: 800px;
  width: 100%;
  max-height: 90vh;
  display: flex;
  flex-direction: column;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 25px 30px;
  border-bottom: 2px solid #eee;
}

.modal-header h2 {
  margin: 0;
  color: #667eea;
}

.btn-fechar {
  background: #f44336;
  color: white;
  border: none;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  font-size: 1.5rem;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-fechar:hover {
  background: #d32f2f;
  transform: rotate(90deg);
}

.modal-conteudo {
  flex: 1;
  overflow-y: auto;
  padding: 30px;
}

.preview-conteudo {
  line-height: 1.8;
}

.preview-conteudo h3 {
  color: #667eea;
  margin-top: 25px;
}

.preview-conteudo ul, .preview-conteudo ol {
  margin-left: 20px;
}

.preview-conteudo li {
  margin: 10px 0;
}

.preview-conteudo em {
  background: #fff3cd;
  padding: 10px 15px;
  border-radius: 8px;
  display: block;
  margin-bottom: 20px;
  border-left: 4px solid #ffc107;
}

.modal-footer {
  padding: 20px 30px;
  border-top: 2px solid #eee;
  text-align: right;
}

.btn-cancelar {
  padding: 12px 30px;
  background: #6c757d;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
}

.btn-cancelar:hover {
  background: #5a6268;
}

@media (max-width: 768px) {
  .recurso-acoes {
    flex-direction: column;
  }

  .dicas-grid {
    grid-template-columns: 1fr;
  }
}
</style>
