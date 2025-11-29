<template>
  <div class="narrativa">
    <AccessibilityMenu />

    <header class="header">
      <button @click="voltar" class="btn-voltar">← Voltar</button>
      <h1>📖 A Missão do Explorador Digital</h1>
    </header>

    <main class="content">
      <div class="historia-intro">
        <div class="avatar-grande">🚀</div>
        <h2>Bem-vindo ao CodeSchool!</h2>
        <p class="subtitulo">Você é um <strong>Explorador Digital</strong>, escolhido para ajudar robôs perdidos a encontrarem o caminho de volta para casa em um mundo de códigos e algoritmos.</p>
      </div>

      <section class="capitulo">
        <div class="capitulo-header">
          <span class="numero">Capítulo 1</span>
          <h3>Primeiros Passos</h3>
        </div>
        <div class="capitulo-conteudo">
          <p>O robô está parado em um grid misterioso. Sua primeira missão é simples: ensinar o robô a dar seus primeiros passos. Cada movimento é um comando que você programa!</p>

          <div class="desafios-grupo">
            <h4>📚 Desafios 1-3: Movimentação Básica</h4>
            <ul>
              <li>🤖 Desafio 1: Primeiros Passos</li>
              <li>↪️ Desafio 2: Virando à Direita</li>
              <li>🔁 Desafio 3: Usando Loops</li>
            </ul>
          </div>
        </div>
      </section>

      <section class="capitulo">
        <div class="capitulo-header">
          <span class="numero">Capítulo 2</span>
          <h3>Otimização de Rotas</h3>
        </div>
        <div class="capitulo-conteudo">
          <p>Agora você descobriu uma forma mais inteligente de dar comandos: usando <strong>loops</strong>! Em vez de repetir comandos manualmente, você pode ensinar o robô a se repetir sozinho.</p>

          <div class="desafios-grupo">
            <h4>📚 Desafios 4-5: Loops e Padrões</h4>
            <ul>
              <li>🔲 Desafio 4: Quadrado Perfeito</li>
              <li>📐 Desafio 5: Corredor em L</li>
            </ul>
          </div>
        </div>
      </section>

      <section class="capitulo">
        <div class="capitulo-header">
          <span class="numero">Capítulo 3</span>
          <h3>Decisões Inteligentes</h3>
        </div>
        <div class="capitulo-conteudo">
          <p>O robô evoluiu! Agora ele tem <strong>sensores</strong> e pode tomar <strong>decisões</strong>. Você vai ensiná-lo a pensar: "Se tem parede na frente, vire!"</p>

          <div class="desafios-grupo">
            <h4>📚 Desafios 6-8: Condicionais e Sensores</h4>
            <ul>
              <li>🪜 Desafio 6: Escadaria (com obstáculos)</li>
              <li>⚡ Desafio 7: Zigue-Zague (decidindo o caminho)</li>
              <li>🔍 Desafio 8: Explorador (usando sensores)</li>
            </ul>
          </div>
        </div>
      </section>

      <section class="capitulo">
        <div class="capitulo-header">
          <span class="numero">Capítulo 4</span>
          <h3>Memória e Variáveis</h3>
        </div>
        <div class="capitulo-conteudo">
          <p>O robô ganhou <strong>memória</strong>! Ele pode contar quantos passos deu, lembrar posições e tomar decisões baseadas em informações passadas.</p>

          <div class="desafios-grupo">
            <h4>📚 Desafios 9-10: Variáveis e Algoritmos Avançados</h4>
            <ul>
              <li>🌀 Desafio 9: Espiral (contando voltas)</li>
              <li>🏆 Desafio 10: Desafio Final (tudo junto!)</li>
            </ul>
          </div>
        </div>
      </section>

      <section class="recompensas">
        <h2>🎁 Conquistas e Recompensas</h2>
        <div class="recompensas-grid">
          <div class="recompensa-card">
            <div class="icone">⭐</div>
            <h4>Ganhe Pontos</h4>
            <p>Cada desafio concluído dá pontos!</p>
          </div>
          <div class="recompensa-card">
            <div class="icone">🎯</div>
            <h4>Suba de Nível</h4>
            <p>De nível 1 até o poderoso nível 5!</p>
          </div>
          <div class="recompensa-card">
            <div class="icone">👑</div>
            <h4>Avatares</h4>
            <p>Desbloqueie avatares incríveis!</p>
          </div>
          <div class="recompensa-card">
            <div class="icone">🏆</div>
            <h4>Badges</h4>
            <p>Conquiste medalhas especiais!</p>
          </div>
          <div class="recompensa-card">
            <div class="icone">📊</div>
            <h4>Ranking</h4>
            <p>Veja sua posição na turma!</p>
          </div>
        </div>
      </section>

      <section class="motivacao">
        <h2>💬 Mensagens do Explorador</h2>
        <div class="frases">
          <div class="frase">
            <span class="emoji">💡</span>
            <p>"Todo programador começou com um 'Olá Mundo'. Você está começando com um robô!"</p>
          </div>
          <div class="frase">
            <span class="emoji">🐛</span>
            <p>"Erros são parte do aprendizado. Até os melhores programadores depuram código todos os dias!"</p>
          </div>
          <div class="frase">
            <span class="emoji">🧠</span>
            <p>"Pense como um cientista da computação: decomponha, identifique padrões, abstraia, crie algoritmos!"</p>
          </div>
          <div class="frase">
            <span class="emoji">🚀</span>
            <p>"Você não está apenas programando um robô. Está aprendendo a pensar computacionalmente!"</p>
          </div>
        </div>
      </section>

      <section class="glossario">
        <h2>📚 Glossário do Explorador</h2>
        <div class="termos">
          <div class="termo">
            <strong>Algoritmo:</strong> Sequência de passos para resolver um problema
          </div>
          <div class="termo">
            <strong>Loop:</strong> Repetição de comandos
          </div>
          <div class="termo">
            <strong>Condicional:</strong> Decisão baseada em uma condição (Se...então...)
          </div>
          <div class="termo">
            <strong>Variável:</strong> Espaço na memória para guardar informação
          </div>
          <div class="termo">
            <strong>Debug:</strong> Encontrar e corrigir erros no código
          </div>
          <div class="termo">
            <strong>Abstração:</strong> Simplificar algo complexo focando no essencial
          </div>
        </div>
      </section>

      <div class="btn-container">
        <button @click="irParaDesafios" class="btn-comecar">🎮 Começar os Desafios!</button>
      </div>
    </main>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'
import AccessibilityMenu from '../components/AccessibilityMenu.vue'

const router = useRouter()

function voltar() {
  router.push('/dashboard')
}

function irParaDesafios() {
  router.push('/dashboard')
}
</script>

<style scoped>
.narrativa {
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
  max-width: 900px;
  margin: 0 auto;
  padding: 40px 20px;
}

.historia-intro {
  background: white;
  border-radius: 20px;
  padding: 40px;
  text-align: center;
  margin-bottom: 30px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.avatar-grande {
  font-size: 5rem;
  margin-bottom: 20px;
}

.historia-intro h2 {
  color: #667eea;
  margin: 0 0 15px 0;
  font-size: 2rem;
}

.subtitulo {
  font-size: 1.2rem;
  color: #555;
  line-height: 1.6;
}

.capitulo {
  background: white;
  border-radius: 15px;
  padding: 30px;
  margin-bottom: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.capitulo-header {
  border-bottom: 3px solid #667eea;
  padding-bottom: 15px;
  margin-bottom: 20px;
}

.numero {
  background: #667eea;
  color: white;
  padding: 5px 15px;
  border-radius: 20px;
  font-size: 0.9rem;
  font-weight: bold;
  display: inline-block;
  margin-bottom: 10px;
}

.capitulo-header h3 {
  margin: 10px 0 0 0;
  color: #333;
  font-size: 1.8rem;
}

.capitulo-conteudo p {
  font-size: 1.1rem;
  line-height: 1.7;
  color: #555;
}

.desafios-grupo {
  background: #f8f9fa;
  padding: 20px;
  border-radius: 10px;
  margin-top: 20px;
}

.desafios-grupo h4 {
  color: #667eea;
  margin-top: 0;
}

.desafios-grupo ul {
  list-style: none;
  padding: 0;
}

.desafios-grupo li {
  padding: 10px;
  background: white;
  margin: 8px 0;
  border-radius: 8px;
  border-left: 4px solid #667eea;
  font-size: 1.05rem;
}

.recompensas {
  background: white;
  border-radius: 15px;
  padding: 30px;
  margin-bottom: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.recompensas h2 {
  color: #667eea;
  text-align: center;
  margin-bottom: 25px;
}

.recompensas-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
  gap: 20px;
}

.recompensa-card {
  text-align: center;
  padding: 20px;
  background: #f8f9fa;
  border-radius: 12px;
  transition: transform 0.3s;
}

.recompensa-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
}

.recompensa-card .icone {
  font-size: 3rem;
  margin-bottom: 10px;
}

.recompensa-card h4 {
  color: #667eea;
  margin: 10px 0;
}

.recompensa-card p {
  font-size: 0.9rem;
  color: #666;
  margin: 0;
}

.motivacao {
  background: white;
  border-radius: 15px;
  padding: 30px;
  margin-bottom: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.motivacao h2 {
  color: #667eea;
  text-align: center;
  margin-bottom: 25px;
}

.frases {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.frase {
  display: flex;
  align-items: center;
  gap: 15px;
  background: #f8f9fa;
  padding: 20px;
  border-radius: 12px;
  border-left: 5px solid #667eea;
}

.frase .emoji {
  font-size: 2.5rem;
  flex-shrink: 0;
}

.frase p {
  margin: 0;
  font-size: 1.1rem;
  color: #555;
  line-height: 1.5;
}

.glossario {
  background: white;
  border-radius: 15px;
  padding: 30px;
  margin-bottom: 25px;
  box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
}

.glossario h2 {
  color: #667eea;
  margin-bottom: 20px;
}

.termos {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 15px;
}

.termo {
  background: #f8f9fa;
  padding: 15px;
  border-radius: 10px;
  border-left: 4px solid #667eea;
}

.termo strong {
  color: #667eea;
  display: block;
  margin-bottom: 5px;
}

.btn-container {
  text-align: center;
  margin-top: 30px;
}

.btn-comecar {
  padding: 20px 50px;
  font-size: 1.3rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  border-radius: 15px;
  cursor: pointer;
  font-weight: bold;
  box-shadow: 0 5px 20px rgba(102, 126, 234, 0.4);
  transition: all 0.3s;
}

.btn-comecar:hover {
  transform: translateY(-3px);
  box-shadow: 0 8px 30px rgba(102, 126, 234, 0.6);
}

@media (max-width: 768px) {
  .recompensas-grid {
    grid-template-columns: repeat(2, 1fr);
  }

  .termos {
    grid-template-columns: 1fr;
  }

  .frase {
    flex-direction: column;
    text-align: center;
  }
}
</style>
