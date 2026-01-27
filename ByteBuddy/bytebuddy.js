/**
 * Globale Variable: Speichert das aktuell gezogene Block-Element
 * Wird beim Dragstart gesetzt und bei Drop-Events verwendet
 */
let draggedBlock = null;

/**
 * Globale Variable: Aktuelles Level
 */
let currentLevel = 0;

/**
 * Level-Konfiguration
 * Definiert alle verfügbaren Level mit ihren Blöcken und korrekter Reihenfolge
 */
const levels = [
    {
        id: 1,
        name: "Systemwartung",
        blocks: [
            { type: "1Step", text: "🔍 Systemstatus prüfen", color: "#4CAF50" },
            { type: "2Step", text: "▶ Dienst starten", color: "#2196F3" },
            { type: "3Step", text: "🌐 Netzwerkverbindung testen", color: "#FF9800" },
            { type: "4Step", text: "❓ Wenn Fehler", color: "#9C27B0" },
            { type: "5Step", text: "🚨 Fehlermeldung senden", color: "#F44336" }
        ],
        correctOrder: ["1Step", "2Step", "3Step", "4Step", "5Step"]
    },
    {
        id: 2,
        name: "Datenbank-Backup",
        blocks: [
            { type: "1Step", text: "💾 Datenbankverbindung prüfen", color: "#4CAF50" },
            { type: "2Step", text: "📦 Backup erstellen", color: "#2196F3" },
            { type: "3Step", text: "✅ Backup validieren", color: "#FF9800" },
            { type: "4Step", text: "❓ Wenn Backup fehlerhaft", color: "#9C27B0" },
            { type: "5Step", text: "🔄 Backup wiederholen", color: "#F44336" }
        ],
        correctOrder: ["1Step", "2Step", "3Step", "4Step", "5Step"]
    }
];

/**
 * Event-Listener für Dragstart
 * Wird auf document-Ebene registriert (Event Delegation)
 * Setzt draggedBlock wenn ein Element mit der Klasse 'block' gezogen wird
 */
document.addEventListener('dragstart', e => {
    if (e.target.classList.contains('block')) {
        draggedBlock = e.target;
        e.dataTransfer.setData('text/plain', 'block');
    }
});

/**
 * Aktiviert Drop-Zonen für Drag & Drop
 * Behandelt drei Szenarien:
 * 1. Palette → Workspace: Block verschieben
 * 2. Workspace → Trash: Block zurück zur Palette
 * 3. Workspace → Workspace: Block neu anordnen
 */
document.querySelectorAll('.dropzone').forEach(zone => {

    // Erlaubt das Droppen von Elementen (preventDefault nötig)
    zone.addEventListener('dragover', e => {
        e.preventDefault();
    });

    // Behandelt das Drop-Event
    zone.addEventListener('drop', e => {
        e.preventDefault();

        if (!draggedBlock) return;

        // Szenario 1: Palette → Workspace: kopieren
        if (zone.id === 'workspace' && draggedBlock.parentElement.id === 'palette') {
            zone.appendChild(draggedBlock);
            return;
        }

        // Szenario 2: Löschen (zurück zur Palette)
        if (zone.id === 'trash') {
            const palette = document.getElementById('palette');

            if (draggedBlock.parentElement.id === 'workspace') {
                palette.appendChild(draggedBlock);
            }
            return;
        }

        // Szenario 3: Innerhalb Workspace neu anordnen
        if (zone.id === 'workspace') {
            const beforeBlock = getInsertPosition(zone, e.clientY);

            if (beforeBlock) {
                zone.insertBefore(draggedBlock, beforeBlock);
            } else {
                zone.appendChild(draggedBlock);
            }

            return;
        }
    });
});

/**
 * Prüft, ob die Blöcke im angegebenen Container in der korrekten Reihenfolge sind
 * @param {string} containerId - ID des zu prüfenden Containers
 * @returns {boolean} True wenn Anzahl UND Reihenfolge korrekt sind
 */
function isCorrectOrder(containerId) {
    const container = document.getElementById(containerId);
    const blocks = container.querySelectorAll('.block');
    const correctOrder = levels[currentLevel].correctOrder;
    
    if (blocks.length !== correctOrder.length) {
        return false;
    }
    
    for (let i = 0; i < correctOrder.length; i++) {
        if (blocks[i].dataset.type !== correctOrder[i]) {
            return false;
        }
    }
    
    return true;
}

/**
 * Liest alle Blöcke aus dem Workspace und gibt deren Typen zurück
 * @returns {Array<string>} Array mit Block-Typen (z.B. ["1Step", "2Step", ...])
 */
function getProgramFromWorkspace() {
    const workspace = document.getElementById('workspace');
    const blocks = workspace.querySelectorAll('.block');

    return Array.from(blocks).map(b => b.dataset.type);
}

/**
 * Führt das übergebene Programm Schritt für Schritt aus
 * Simuliert verschiedene System-Operationen und behandelt If-Logik
 * @param {Array<string>} program - Array von Schritt-IDs die ausgeführt werden sollen
 */
function executeProgram(program) {
    let errorOccurred = false;

    for (let i = 0; i < program.length; i++) {
        const step = program[i];

        switch (step) {
            case "1Step":
                console.log("🔍 Systemstatus prüfen");
                // simuliert: Fehler zufällig
                errorOccurred = Math.random() < 0.5;
                console.log(errorOccurred ? "❌ Fehler gefunden" : "✅ System OK");
                break;

            case "2Step":
                console.log("▶ Dienst starten");
                break;

            case "3Step":
                console.log("🌐 Netzwerkverbindung testen");
                break;

            case "4Step":
                console.log("❓ Wenn Fehler");
                // Wenn kein Fehler: überspringt den nächsten Schritt (5Step)
                if (!errorOccurred) {
                    alert("RICHTIG!");
                    i++; // überspringt 5Step
                }
                break;

            case "5Step":
                console.log("🚨 Fehlermeldung senden");
                break;
        }
    }
}

/**
 * Initialisiert ein Level
 * Lädt die Blöcke in die Palette und mischt sie
 * @param {number} levelIndex - Index des zu ladenden Levels
 */
function initLevel(levelIndex) {
    const level = levels[levelIndex];
    const palette = document.getElementById('palette');
    const workspace = document.getElementById('workspace');
    
    // Workspace leeren
    workspace.innerHTML = '';
    
    // Palette leeren und neu befüllen
    palette.innerHTML = '';
    
    level.blocks.forEach(blockData => {
        const block = document.createElement('div');
        block.className = 'block';
        block.setAttribute('draggable', 'true');
        block.dataset.type = blockData.type;
        block.textContent = blockData.text;
        block.style.backgroundColor = blockData.color;
        palette.appendChild(block);
    });
    
    // Palette mischen
    shufflePalette();
    
    // Level-Titel aktualisieren (wenn vorhanden)
    const levelTitle = document.getElementById('levelTitle');
    if (levelTitle) {
        levelTitle.textContent = `Level ${level.id}: ${level.name}`;
    }
}

/**
 * Berechnet die Einfügeposition für ein gezogenes Element
 * basierend auf der vertikalen Mausposition
 * @param {HTMLElement} container - Der Container in den eingefügt werden soll
 * @param {number} mouseY - Y-Koordinate der Maus
 * @returns {HTMLElement|null} Element vor dem eingefügt werden soll, oder null für Ende
 */
function getInsertPosition(container, mouseY) {
    const blocks = container.querySelectorAll('.block:not(.dragging)');

    for (let block of blocks) {
        const rect = block.getBoundingClientRect();
        const middle = rect.top + rect.height / 2;

        // Wenn Maus über der Mitte des Blocks ist: davor einfügen
        if (mouseY < middle) {
            return block; // davor einfügen
        }
    }

    return null; // ganz unten einfügen
}

/**
 * Mischt die Blöcke in der Palette zufällig
 * Wird beim Laden der Seite aufgerufen
 */
function shufflePalette() {
    const palette = document.getElementById('palette');
    const blocks = Array.from(palette.querySelectorAll('.block'));
    
    // Fisher-Yates Shuffle Algorithmus
    for (let i = blocks.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [blocks[i], blocks[j]] = [blocks[j], blocks[i]];
    }
    
    // Blöcke in neuer Reihenfolge wieder einfügen
    blocks.forEach(block => palette.appendChild(block));
}

/**
 * Event-Listener für den "Ausführen"-Button
 * Validiert die Anordnung und führt das Programm aus
 */
document.getElementById('runBtn').addEventListener('click', () => {
    const program = getProgramFromWorkspace();
    
    if (isCorrectOrder("workspace")) {
        executeProgram(program);
        
        // Zum nächsten Level
        if (currentLevel < levels.length - 1) {
            setTimeout(() => {
                if (confirm(`🎉 Level ${currentLevel + 1} geschafft! Weiter zu Level ${currentLevel + 2}?`)) {
                    currentLevel++;
                    initLevel(currentLevel);
                }
            }, 500);
        } else {
            setTimeout(() => {
                alert("🏆 Glückwunsch! Alle Level geschafft!");
            }, 500);
        }
    } else {
        alert("❌ Blöcke sind nicht korrekt angeordnet!");
    }
});

/**
 * Initialisierung beim Laden der Seite
 * Lädt das erste Level
 */
window.addEventListener('DOMContentLoaded', () => {
    initLevel(currentLevel);
});






/** ALLE KOMMENTARE WURDE VON DER CLAUDE AI ERSTELLT! */